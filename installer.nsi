!define PRODUCT_NAME "AllSkyCondition"
!define PRODUCT_VERSION "1.0.0.0"
!define PRODUCT_PUBLISHER "Astro Vino"
!define DRIVER_DLL "ASCOM.AllSkyCondition.SafetyMonitor.dll"
!define DRIVER_ID "ASCOM.AllSkyCondition.SafetyMonitor"

; MUI 2.0
!include "MUI2.nsh"

; Installer properties
Name "${PRODUCT_NAME} Safety Monitor"
OutFile "${PRODUCT_NAME}_v${PRODUCT_VERSION}.exe"
InstallDir "$PROGRAMFILES64\Common Files\ASCOM\SafetyMonitor"
InstallDirRegKey HKLM "Software\${DRIVER_ID}" "InstallDir"
RequestExecutionLevel admin

; Interface Settings
!define MUI_ABORTWARNING

; Pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

; Languages
!insertmacro MUI_LANGUAGE "English"

Section "MainSection" SEC01
    SetOutPath "$INSTDIR"
    SetOverwrite ifnewer

    ; Add files
    File "bin\x64\Release\${DRIVER_DLL}"
    File "bin\x64\Release\model.onnx"
    File "bin\x64\Release\ASCOM.Astrometry.dll"
    File "bin\x64\Release\ASCOM.Attributes.dll"
    File "bin\x64\Release\ASCOM.DeviceInterfaces.dll"
    File "bin\x64\Release\ASCOM.Exceptions.dll"
    File "bin\x64\Release\ASCOM.Utilities.dll"
    File "bin\x64\Release\Microsoft.ML.OnnxRuntime.dll"
    File "bin\x64\Release\System.Buffers.dll"
    File "bin\x64\Release\System.Drawing.Common.dll"
    File "bin\x64\Release\System.Memory.dll"
    File "bin\x64\Release\System.Numerics.Vectors.dll"
    File "bin\x64\Release\System.Runtime.CompilerServices.Unsafe.dll"
    File "bin\x64\Release\onnxruntime.dll"
    File "bin\x64\Release\onnxruntime_providers_shared.dll"

    ; Register the DLL
    ExecWait '"$WINDIR\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" "$INSTDIR\${DRIVER_DLL}" /codebase'

    ; Write the installation path to the registry
    WriteRegStr HKLM "SOFTWARE\${DRIVER_ID}" "InstallDir" "$INSTDIR"

    ; Write the uninstall keys for Windows
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "DisplayName" "ASCOM SafetyMonitor Driver for ${PRODUCT_NAME}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "UninstallString" '"$INSTDIR\uninstall.exe"'
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "DisplayVersion" "${PRODUCT_VERSION}"
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "Publisher" "${PRODUCT_PUBLISHER}"
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "NoModify" 1
    WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "NoRepair" 1
    WriteUninstaller "$INSTDIR\uninstall.exe"
SectionEnd

Section "Uninstall"
    ; Unregister the DLL
    ExecWait '"$WINDIR\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" /unregister "$INSTDIR\${DRIVER_DLL}"'

    ; Remove files
    Delete "$INSTDIR\${DRIVER_DLL}"
    Delete "$INSTDIR\model.onnx"
    Delete "$INSTDIR\ASCOM.Astrometry.dll"
    Delete "$INSTDIR\ASCOM.Attributes.dll"
    Delete "$INSTDIR\ASCOM.DeviceInterfaces.dll"
    Delete "$INSTDIR\ASCOM.Exceptions.dll"
    Delete "$INSTDIR\ASCOM.Utilities.dll"
    Delete "$INSTDIR\Microsoft.ML.OnnxRuntime.dll"
    Delete "$INSTDIR\System.Buffers.dll"
    Delete "$INSTDIR\System.Drawing.Common.dll"
    Delete "$INSTDIR\System.Memory.dll"
    Delete "$INSTDIR\System.Numerics.Vectors.dll"
    Delete "$INSTDIR\System.Runtime.CompilerServices.Unsafe.dll"
    Delete "$INSTDIR\onnxruntime.dll"
    Delete "$INSTDIR\onnxruntime_providers_shared.dll"
    Delete "$INSTDIR\uninstall.exe"

    ; Remove directory
    RMDir "$INSTDIR"

    ; Remove registry keys
    DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
    DeleteRegKey HKLM "SOFTWARE\${DRIVER_ID}"
SectionEnd
