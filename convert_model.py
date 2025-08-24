# This script converts the Keras .h5 model to the ONNX format for use in C#.
# You will need to install tensorflow and tf2onnx: 
# pip install tensorflow tf2onnx

import tensorflow as tf
import tf2onnx

# Load the Keras model
try:
    model = tf.keras.models.load_model('keras_model.h5', compile=False)
    print("Keras model loaded successfully.")
except Exception as e:
    print(f"Error loading Keras model: {e}")
    exit()

# Define the input signature for the model
# This should match the input shape your model expects.
# From AllSkyCondition.py, it's (1, 224, 224, 3).
input_signature = [tf.TensorSpec(model.inputs[0].shape, model.inputs[0].dtype, name='input')]

# Convert the model to ONNX
# opset=13 is a stable choice for broad compatibility.
output_path = "model.onnx"
try:
    model_proto, _ = tf2onnx.convert.from_keras(model, input_signature, opset=13)
    with open(output_path, "wb") as f:
        f.write(model_proto.SerializeToString())
    print(f"Model successfully converted to {output_path}")
except Exception as e:
    print(f"Error during ONNX conversion: {e}")
