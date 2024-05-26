import os

# Estructura de carpetas
structure = {
    "src": {
        "Core": {
            "Domain": ["Entities", "Interfaces", "Services"],
            "Application": ["DTOs", "Interfaces", "Services"],
        },
        "Infrastructure": {
            "Data": ["Entities", "Repositories", "Contexts"],
            "Services": ["Implementations"],
        },
        "Presentation": {
            "API": ["Controllers", "Models"],
        },
        "CrossCutting": ["IoC", "Logging", "Common"],
    },
    "tests": {
        "UnitTests": ["Core", "Infrastructure"],
        "IntegrationTests": ["Core", "Infrastructure"],
        "EndToEndTests": [],
    },
    "build": [],
    "docs": [],
}

# Archivos adicionales
files = [".editorconfig", ".gitignore", "README.md"]

def create_structure(base_path, structure):
    for name, content in structure.items():
        path = os.path.join(base_path, name)
        os.makedirs(path, exist_ok=True)
        if isinstance(content, dict):
            create_structure(path, content)
        elif isinstance(content, list):
            for item in content:
                os.makedirs(os.path.join(path, item), exist_ok=True)

def create_files(base_path, files):
    for file in files:
        open(os.path.join(base_path, file), 'w').close()

base_path = "."  # Directorio base, puede ser cambiado según necesidad

create_structure(base_path, structure)
create_files(base_path, files)

print("Estructura de carpetas y archivos creada exitosamente.")
