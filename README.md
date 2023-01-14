Esta es una aplicacion web rest de publicaciones y comentarios, tiene jwt login hassheando la password, limitacions de peticiones por ip, bloqueo en el swagger para consumir
algunos endpoints, se necesita ej jwt bearer, verficacion simple del estado de la aplicacion con el servicio healty, orm dapper, paginacion utilizando store procedure, patron de diseño inyeccion de independia
clean architecture, la base de datos esta alojada en azure con microsfot sql server. Tiene incluido el archivo docker file que la imagen ya esta subido a docker hub, para
probar la aplicacion utlizar el comando docker run -p 8080:80 -d palomomatias/socialmedia y abrir en el explorador http://localhost:8080/swagger/index.html
