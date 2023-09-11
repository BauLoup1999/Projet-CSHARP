from flask import Flask, request, jsonify

app = Flask(__name__)

# Données de test pour les sites
sites = [
    {"ID": 1, "ville": "Paris"},
    {"ID": 2, "ville": "Nantes"},
    # Ajoute d'autres sites ici
]

# Endpoint pour récupérer tous les sites
@app.route('/sites', methods=['GET'])
def get_sites():
    return jsonify(sites)

# Endpoint pour créer un nouveau site
@app.route('/sites', methods=['POST'])
def create_site():
    new_site = request.json
    new_site["ID"] = len(sites) + 1  # Génère un nouvel ID
    sites.append(new_site)
    return jsonify(new_site), 201

# Endpoint pour mettre à jour un site existant
@app.route('/sites/<int:site_id>', methods=['PUT'])
def update_site(site_id):
    for site in sites:
        if site["ID"] == site_id:
            site.update(request.json)
            return jsonify(site)
    return "Site non trouvé", 404

# Endpoint pour supprimer un site
@app.route('/sites/<int:site_id>', methods=['DELETE'])
def delete_site(site_id):
    global sites
    sites = [site for site in sites if site["ID"] != site_id]
    return "Site supprimé", 200

if __name__ == '__main__':
    app.run(debug=True)
