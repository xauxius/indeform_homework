import torch
from torch.utils.data import Dataset
from torchvision.transforms import v2
from PIL import Image
import io

class RCNNDataset(Dataset):
    def __init__(self, storage_client, dataset_id, dataset_type):
        self.storage_client = storage_client
        self.images = storage_client.get_dataset(dataset_id, dataset_type).json()

    def __len__(self):
        return len(self.images)

    def __getitem__(self, idx):
        image_id = self.images[idx]

        image_response = self.storage_client.get_image(image_id)
        PIL_image = Image.open(io.BytesIO(image_response.content))
        t = v2.Compose([
            v2.ToImage(),
            v2.ToDtype(torch.float32)
        ])
        image = t(PIL_image)

        labels_response = self.storage_client.get_labels(image_id)
        labels = labels_response.json()

        target_boxes = torch.zeros((len(labels), 4), dtype=torch.float32)
        target_labels = torch.zeros(len(labels), dtype=torch.int64)

        for i, label in enumerate(labels):
            target_labels[i] = label["class"] + 1
            target_boxes[i, 0] = label["x"]
            target_boxes[i, 1] = label["y"]
            target_boxes[i, 2] = target_boxes[i, 0] + label["w"]
            target_boxes[i, 3] = target_boxes[i, 1] + label["h"]

        target = {"boxes": target_boxes, "labels": target_labels}
        
        return image, target