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
    
    def post_dataset_entry(self, image_id, dataset_id, set_type):
        response = requests.post(self.path(f"/dataset/{dataset_id}/image/{image_id}/{set_type}"))
        return response

storageClient = StorageClient()
        