use('DecoAR');
db.Users.insertOne({
      "correo": "prueba3@gmail.com",
      "password": "12453446",
      "cat1": 2,
      "cat2": 1,
      "cat3": 7
});

use('DecoAR');
db.Users.find({
    "correo": "prueba3@gmail.com",
    "password": "12453446"
});