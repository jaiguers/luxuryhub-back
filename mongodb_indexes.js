// MongoDB Indexes for LuxuryHub - Optimized for 1M+ records
// Execute this script in MongoDB shell or MongoDB Compass

// Database: luxuryhub

// Properties Collection Indexes
db.properties.createIndex({ "createdAt": -1 }, { background: true, name: "idx_properties_createdAt_desc" });
db.properties.createIndex({ "idOwner": 1 }, { background: true, name: "idx_properties_idOwner" });
db.properties.createIndex({ "price": 1 }, { background: true, name: "idx_properties_price_asc" });
db.properties.createIndex({ "price": -1 }, { background: true, name: "idx_properties_price_desc" });
db.properties.createIndex({ "name": "text", "address": "text" }, { background: true, name: "idx_properties_text_search" });
db.properties.createIndex({ "codeInternal": 1 }, { background: true, unique: true, name: "idx_properties_codeInternal_unique" });

// Compound indexes for common queries
db.properties.createIndex({ "price": 1, "createdAt": -1 }, { background: true, name: "idx_properties_price_createdAt" });
db.properties.createIndex({ "idOwner": 1, "createdAt": -1 }, { background: true, name: "idx_properties_idOwner_createdAt" });

// PropertyImages Collection Indexes
db.propertyImages.createIndex({ "idProperty": 1, "enabled": 1 }, { background: true, name: "idx_propertyImages_idProperty_enabled" });
db.propertyImages.createIndex({ "idProperty": 1, "createdAt": 1 }, { background: true, name: "idx_propertyImages_idProperty_createdAt" });
db.propertyImages.createIndex({ "enabled": 1 }, { background: true, name: "idx_propertyImages_enabled" });

// Owners Collection Indexes
db.owners.createIndex({ "name": 1 }, { background: true, name: "idx_owners_name" });
db.owners.createIndex({ "createdAt": -1 }, { background: true, name: "idx_owners_createdAt" });

// PropertyTraces Collection Indexes
db.propertyTraces.createIndex({ "idProperty": 1, "dateSale": -1 }, { background: true, name: "idx_propertyTraces_idProperty_dateSale" });
db.propertyTraces.createIndex({ "dateSale": -1 }, { background: true, name: "idx_propertyTraces_dateSale" });

// Show all indexes
print("=== Properties Indexes ===");
db.properties.getIndexes().forEach(function(idx) { printjson(idx); });

print("=== PropertyImages Indexes ===");
db.propertyImages.getIndexes().forEach(function(idx) { printjson(idx); });

print("=== Owners Indexes ===");
db.owners.getIndexes().forEach(function(idx) { printjson(idx); });

print("=== PropertyTraces Indexes ===");
db.propertyTraces.getIndexes().forEach(function(idx) { printjson(idx); });
