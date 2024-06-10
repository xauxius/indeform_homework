import sys
import io
import os
import torch

project_root = os.path.abspath(os.path.join(os.curdir, '../'))
if project_root not in sys.path:
    sys.path.append(project_root)

from common.storage_client import storage_client
from model import Model

def load_all_models():
    all_models_info = storage_client.get_all_model_info().json()
    models = []

    for model_info in all_models_info:
        model_response = storage_client.download_model(model_info["id"])
        model = Model(torch.load(io.BytesIO(model_response.content)), model_info["id"], model_info["name"])
        models.append(model)
    return models

def get_model_by_id(models, id):
    for model in models:
        if model.id == id:
            return model