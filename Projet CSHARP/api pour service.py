# Données de test pour les services
services = [
    {"ID": 1, "nom": "Comptabilité"},
    {"ID": 2, "nom": "Production"},
    # Ajoute d'autres services ici
]

# Endpoint pour récupérer tous les services
@app.route('/services', methods=['GET'])
def get_services():
    return jsonify(services)

# Endpoint pour créer un nouveau service
@app.route('/services', methods=['POST'])
def create_service():
    new_service = request.json
    new_service["ID"] = len(services) + 1  # Génère un nouvel ID
    services.append(new_service)
    return jsonify(new_service), 201

# Endpoint pour mettre à jour un service existant
@app.route('/services/<int:service_id>', methods=['PUT'])
def update_service(service_id):
    for service in services:
        if service["ID"] == service_id:
            service.update(request.json)
            return jsonify(service)
    return "Service non trouvé", 404

# Endpoint pour supprimer un service
@app.route('/services/<int:service_id>', methods=['DELETE'])
def delete_service(service_id):
    global services
    services = [service for service in services if service["ID"] != service_id]
    return "Service supprimé", 200
