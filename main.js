const { MongoClient } = require("mongodb");
const Express = require("express");
const BodyParser = require('body-parser');

const server = Express();
const client = new MongoClient(process.env.ATLAS_URI);
//const client = new MongoClient(process.env['mongodb+srv://FridaLaDogga:1105@decoar.rhxqfhb.mongodb.net/?retryWrites=true&w=majority&appName=DecoAR']);

server.use(BodyParser.json());

var collection;

//server.get("/", (request, response) => { response.send("Bienvenido a la API de conexión con mongodb de DecoAR");});

server.post("/Users", async (request, response, next) => {
    try {
        let correo = request.body.correo;
        let password = request.body.password;
        let cat1 = 0;
        let cat2 = 0;
        let cat3 = 0;

        console.log(request.body); // Verificar que se estén recibiendo los datos en el servidor

        let result = await collection.insertOne({ correo, password, cat1, cat2, cat3 });

        response.status(201).send({ message: "Usuario creado correctamente" });
        console.log(`Correo: ${correo}, Password: ${password}, Cat1: ${cat1}, Cat2: ${cat2}, Cat3: ${cat3}`);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});


server.get("/Users", async (request, response, next) => {
    try {
        let correo = request.query.correo; // Obtener el correo proporcionado por el usuario
        let password = request.query.password; // Obtener la contraseña proporcionada por el usuario

        // Comprobar si se proporcionaron el correo y la contraseña
        if (!correo || !password) {
            response.status(400).send({ message: "Debes proporcionar un correo y una contraseña." });
            return;
        }

        // Buscar un usuario que coincida exactamente con el correo y la contraseña proporcionados
        let result = await collection.find({ correo: correo, password: password }).toArray();

        if (result.length === 0) {
            response.status(404).send({ message: "Usuario no encontrado con el correo y contraseña proporcionados." });
        } else {
            response.send(result);
        }
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.listen("3000", async() => {
    try{
        await client.connect();
        collection = client.db("DecoAR").collection("Users");
        console.log("Listening at :3000...");
    } catch (e) {
        console.error(e);
    }
});