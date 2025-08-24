// AllSkyCondition SafetyMonitor Driver
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using ASCOM.Utilities.Exceptions;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Collections.Generic;
using System.Linq;

namespace ASCOM.AllSkyCondition
{
    [Guid("a8f9c218-c27d-4209-81f1-91a0b39f6d6f")]
    [ProgId(SafetyMonitor.DRIVER_ID)]
    [ServedClassName("AllSky Condition Safety Monitor")]
    [ClassInterface(ClassInterfaceType.None)]
    public class SafetyMonitor : ISafetyMonitor
    {
        #region ASCOM Registration
        // Your driver's DeviceID is ASCOM.AllSkyCondition.SafetyMonitor
        // The Guid attribute sets the CLSID for ASCOM.
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "SafetyMonitor";
                if (bRegister)
                {
                    P.Register(DRIVER_ID, "AllSky Condition Safety Monitor");
                }
                else
                {
                    P.Unregister(DRIVER_ID);
                }
            }
        }

        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }
        #endregion
        internal const string DRIVER_ID = "ASCOM.AllSkyCondition.SafetyMonitor";
        internal static string DRIVER_PROFILENAME = "ASCOM.AllSkyCondition.SafetyMonitor";

        private System.Threading.Timer _checkTimer;
        private InferenceSession _onnxSession;
        private readonly string[] _labels = { "Clear", "Cloudy", "Covered", "Rainy" };

        // State variables
        private bool _isSafe = true;
        private string _statusMessage = "Initializing";
        private int _deltaCloudy = 0;
        private int _deltaCovered = 0;

        // Configuration
        internal string ImagePath { get; set; } = "";
        internal int CycleTime { get; set; } = 1;
        internal int CloudyTolerance { get; set; } = 2;
        internal int CoveredTolerance { get; set; } = 2;
        internal bool DebugLog { get; set; } = false;

        public SafetyMonitor()
        {
            LoadProfile();
        }

        private void InitializeOnnx()
        {
            try
            {
                // The model.onnx file must be in the same directory as the DLL.
                string modelPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "model.onnx");
                if (!File.Exists(modelPath))
                {
                    _statusMessage = "ERROR: model.onnx not found.";
                    _isSafe = false;
                    return;
                }
                _onnxSession = new InferenceSession(modelPath);
                _statusMessage = "Ready to monitor.";
            }
            catch (Exception ex)
            {
                _statusMessage = $"ONNX Error: {ex.Message}";
                _isSafe = false;
            }
        }

        private void StartMonitoring()
        {
            _checkTimer = new System.Threading.Timer(CheckSafety, null, TimeSpan.Zero, TimeSpan.FromMinutes(CycleTime));
        }

        private void CheckSafety(object state)
        {
            if (_onnxSession == null || string.IsNullOrEmpty(ImagePath) || !File.Exists(ImagePath))
            {
                _isSafe = false;
                _statusMessage = "Unsafe: Invalid config or model.";
                return;
            }

            try
            {
                // 1. Image Processing
                using (var image = (Bitmap)Image.FromFile(ImagePath))
                {
                    // Convert to grayscale and then back to 3-channel BGR for the model
                    Bitmap processedImage = PreprocessImage(image);

                    // 2. Prepare Tensor
                    var inputTensor = new DenseTensor<float>(new[] { 1, 224, 224, 3 });
                    for (int y = 0; y < 224; y++)
                    {
                        for (int x = 0; x < 224; x++)
                        {
                            var pixel = processedImage.GetPixel(x, y);
                            // Normalize to [-1, 1]
                            inputTensor[0, y, x, 0] = (pixel.R / 127.0f) - 1.0f;
                            inputTensor[0, y, x, 1] = (pixel.G / 127.0f) - 1.0f;
                            inputTensor[0, y, x, 2] = (pixel.B / 127.0f) - 1.0f;
                        }
                    }

                    // 3. Run Inference
                    var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor("input", inputTensor) };
                    using (var results = _onnxSession.Run(inputs))
                    {
                        var output = results.FirstOrDefault()?.AsTensor<float>().ToArray();
                        if (output == null) return;

                        int predictionIndex = Array.IndexOf(output, output.Max());
                        string skyState = _labels[predictionIndex];

                        // 4. Update Safety Logic
                        UpdateSafetyState(predictionIndex, skyState);
                    }
                }
            }
            catch (Exception ex)
            {
                _isSafe = false;
                _statusMessage = $"Check Error: {ex.Message}";
            }
        }
        
        private Bitmap PreprocessImage(Bitmap original)
        {
            // This mimics the Python script's processing steps
            // Grayscale -> Colorize -> Fit -> Crop
            var gray = new Bitmap(original.Width, original.Height, PixelFormat.Format8bppIndexed);
            using (var g = Graphics.FromImage(gray))
            {
                // Grayscale conversion matrix
                var colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
                using (var attributes = new ImageAttributes()) {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            // Colorize (this step is odd, but we replicate it)
            var colorized = new Bitmap(gray.Width, gray.Height, PixelFormat.Format24bppRgb);
            using(var g = Graphics.FromImage(colorized)) {
                g.DrawImage(gray, 0, 0);
            }

            // Fit and Crop
            int w = colorized.Width, h = colorized.Height;
            int nw = (int)(224.0 * w / h);
            var resized = new Bitmap(nw, 224);
            using (var g = Graphics.FromImage(resized)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(colorized, 0, 0, nw, 224);
            }

            int left = (int)(nw / 2.0 - 112);
            var cropRect = new Rectangle(left, 0, 224, 224);
            return resized.Clone(cropRect, resized.PixelFormat);
        }

        private void UpdateSafetyState(int classIndex, string skyState)
        {
            string statusText = "";
            if (classIndex == 0) // Clear
            {
                _deltaCloudy = 0;
                _deltaCovered = 0;
                _isSafe = true;
                statusText = "Clear: GO";
            }
            else if (classIndex == 1) // Cloudy
            {
                _deltaCloudy += CycleTime;
                _deltaCovered = 0;
                _isSafe = _deltaCloudy <= CloudyTolerance;
                statusText = _isSafe ? $"Cloudy: PAUSE (in tolerance)" : $"Cloudy: STOP (tolerance exceeded)";
            }
            else if (classIndex == 2) // Covered
            {
                _deltaCloudy += CycleTime;
                _deltaCovered += CycleTime;
                _isSafe = _deltaCovered <= CoveredTolerance && _deltaCloudy <= CloudyTolerance;
                statusText = _isSafe ? "Covered: PAUSE (in tolerance)" : "Covered: STOP (tolerance exceeded)";
            }
            else // Rainy
            {
                _isSafe = false;
                statusText = "Rainy: STOP";
            }
            _statusMessage = $"{DateTime.Now:HH:mm:ss} - {statusText}";
            if (DebugLog) SharedResources.tl.LogMessage("UpdateSafetyState", _statusMessage);
        }

        #region ISafetyMonitor Implementation

        public bool IsSafe => _isSafe;

        public string Action(string actionName, string actionParameters)
        {
            if (string.Equals(actionName, "status", StringComparison.OrdinalIgnoreCase))
            {
                return _statusMessage;
            }
            throw new MethodNotImplementedException($"Action '{actionName}' is not implemented.");
        }

        public void CommandBlind(string command, bool raw)
        {
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public bool Connected
        {
            get => _onnxSession != null;
            set
            {
                if (value)
                {
                    if (_onnxSession == null)
                    {
                        LoadProfile(); // Reload profile to get latest settings
                        SharedResources.tl.Enabled = DebugLog; // Enable/disable logger
                        InitializeOnnx();
                        StartMonitoring();
                    }
                }
                else
                {
                    if (_onnxSession != null)
                    {
                        _checkTimer?.Dispose();
                        _checkTimer = null;
                        _onnxSession?.Dispose();
                        _onnxSession = null;
                    }
                }
            }
        }

        public string Description => "AllSky Condition Safety Monitor";

        public string DriverInfo => $"Monitors sky condition via image analysis. Status: {_statusMessage}";

        public string DriverVersion => "1.0";

        public short InterfaceVersion => 3;

        public string Name => "AllSkyCondition Monitor";

        public ArrayList SupportedActions => new ArrayList() { "status" };

        public void SetupDialog()
        {
            LoadProfile(); // Ensure the latest settings are loaded before showing the dialog
            using (var f = new SetupDialogForm(this))
            {
                f.ShowDialog();
            }
        }

        public void Dispose()
        {
            _checkTimer?.Dispose();
            _onnxSession?.Dispose();
        }

        #endregion

        #region Profile Management
        internal void LoadProfile()
        {
            try
            {
                using (Profile p = new Profile())
                {
                    p.DeviceType = "SafetyMonitor";
                    ImagePath = p.GetValue(DRIVER_ID, "ImagePath", string.Empty, "");
                    CycleTime = Convert.ToInt32(p.GetValue(DRIVER_ID, "CycleTime", string.Empty, "1"));
                    CloudyTolerance = Convert.ToInt32(p.GetValue(DRIVER_ID, "CloudyTolerance", string.Empty, "2"));
                    CoveredTolerance = Convert.ToInt32(p.GetValue(DRIVER_ID, "CoveredTolerance", string.Empty, "2"));
                    DebugLog = Convert.ToBoolean(p.GetValue(DRIVER_ID, "DebugLog", string.Empty, "False"));
                }
            }
            catch (Exception ex)
            {
                _statusMessage = $"Error loading profile: {ex.Message}";
            }
        }

        internal void SaveProfile()
        {
            try
            {
                using (Profile p = new Profile())
                {
                    p.DeviceType = "SafetyMonitor";
                    p.WriteValue(DRIVER_ID, "ImagePath", ImagePath);
                    p.WriteValue(DRIVER_ID, "CycleTime", CycleTime.ToString());
                    p.WriteValue(DRIVER_ID, "CloudyTolerance", CloudyTolerance.ToString());
                    p.WriteValue(DRIVER_ID, "CoveredTolerance", CoveredTolerance.ToString());
                    p.WriteValue(DRIVER_ID, "DebugLog", DebugLog.ToString());
                }
            }
            catch (Exception ex)
            {
                _statusMessage = $"Error saving profile: {ex.Message}";
            }
        }
        #endregion
    }
}
