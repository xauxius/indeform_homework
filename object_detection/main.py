from fastapi import FastAPI, File, UploadFile, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from utils import load_all_models, get_model_by_id
from PIL import Image
import io

models = load_all_models()

app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.get("/models")
async def get_models():
    return [model.get_response_dto() for model in models]

@app.post("/predict/{model_id}")
async def predict(model_id: int, image: UploadFile = File(...)):
    contents = await image.read()
    image = Image.open(io.BytesIO(contents))

    model = get_model_by_id(models, model_id)
    if model == None:
        raise HTTPException(404, "Model not found")
    
    bounding_boxes = model.get_bounding_boxes(image)

    return bounding_boxes