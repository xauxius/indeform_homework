import torch
from PIL import Image
import io
from torchvision.transforms import v2

class Model:
    def __init__(self, model, id, name):
        self.model = model
        self.model.eval()
        self.device = torch.device("cuda") if torch.cuda.is_available() else torch.device("cpu")
        self.model.to(self.device)
        self.id = id
        self.name = name

    def get_response_dto(self):
        return {
            "id": self.id,
            "name": self.name
        }
    
    def get_bounding_boxes(self, image):
        try:
            transform = v2.Compose([
                v2.ToImage(),
                v2.ToDtype(torch.float32)
            ])
            image = transform(image).to(self.device)
            pred = self.model([image])
            print(pred)
        except Exception as error:
            print(error)

        return [{ # palikta testavimo tikslams, turint veikiančius modelius bus sutvarkyta
            "score": 80,
            "x1": 200,
            "y1": 300,
            "x2": 400,
            "y2": 500
        }]