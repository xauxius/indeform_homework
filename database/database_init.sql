CREATE TABLE Image (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    source_path VARCHAR(255) NOT NULL,
    date_added DATETIME NOT NULL
);

CREATE TABLE Label (
    id INT PRIMARY KEY AUTO_INCREMENT,
    image_id INT NOT NULL,

    class ENUM("Herring") NOT NULL,
    x FLOAT NOT NULL,
    y FLOAT NOT NULL,
    w FLOAT NOT NULL,
    h FLOAT NOT NULL,

    FOREIGN KEY (image_id) REFERENCES Image(id)
);

CREATE TABLE Dataset (
    id INT PRIMARY KEY AUTO_INCREMENT,

    created_date DATETIME NOT NULL,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE Model (
    id INT PRIMARY KEY AUTO_INCREMENT,
    train_dataset_id int,
    start_model_id int,

    name VARCHAR(255) NOT NULL,
    source_path VARCHAR(255) NOT NULL,

    FOREIGN KEY (train_dataset_id) REFERENCES Dataset(id),
    FOREIGN KEY (start_model_id) REFERENCES Model(id)
);

CREATE TABLE Dataset_Entry (
    dataset_id INT NOT NULL,
    image_id INT NOT NULL,
    set_type ENUM("Training", "Test", "Validation") NOT NULL,

    PRIMARY KEY (dataset_id, image_id),
    FOREIGN KEY (dataset_id) REFERENCES Dataset(id),
    FOREIGN KEY (image_id) REFERENCES Image(id)
);