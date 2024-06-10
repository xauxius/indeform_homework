import requests
from .config import STORAGE_ADDRESS

class StorageClient:
    def __init__(self):
        self.root_address = STORAGE_ADDRESS

    def path(self, params):
        return self.root_address + params

    def post_image(self, image, image_name):
        files = {'image': (image_name, image, 'multipart/form-data') }
        response = requests.post(self.path("/images"), files=files)
        return response
    
    def get_image(self, id):
        response = requests.get(self.path(f"/images/download/{id}"))
        return response
    
    def post_label(self, label, image_id):
        response = requests.post(self.path(f"/labels/{image_id}"), data=label)
        return response
    
    def get_labels(self, image_id):
        response = requests.get(self.path(f"/labels/{image_id}"))
        return response
    
    def get_dataset(self, dataset_id, set_type):
        response = requests.get(self.path(f"/dataset/{dataset_id}/{set_type}"))
        return response
    
    def post_dataset_entry(self, image_id, dataset_id, set_type):
        response = requests.post(self.path(f"/dataset/{dataset_id}/{set_type}/image/{image_id}"))
        return response
    
    def post_model(self, model_file, create_model_dto, file_name):
        files = {"modelFile": (file_name, model_file, "multipart/form-data")}
        response = requests.post(self.path("/models"), files=files, data=create_model_dto)
        return response
    
    def get_all_model_info(self):
        response = requests.get(self.path("/models"))
        return response
    
    def download_model(self, model_id):
        response = requests.get(self.path(f"/models/download/{model_id}"))
        return response

storage_client = StorageClient()
