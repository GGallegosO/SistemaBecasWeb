# Sistema de Gestión y Evaluación de Becas 🎓

Plataforma web desarrollada en **ASP.NET Core MVC** para la administración de solicitudes de becas estudiantiles. El sistema automatiza el cálculo de puntajes basado en reglas de negocio (promedio de notas, ingreso familiar y situación laboral) y protege el acceso mediante autenticación de sesiones cifradas.

---

## 🔒 Seguridad en el Backend y HTTPS

### Seguridad Mínima (Cookies/Auth)
El sistema implementa autenticación basada en **Cookies (`CookieAuth`)**. Los endpoints sensibles (como la lectura, edición y eliminación de becas) están protegidos mediante el atributo `[Authorize]`. 
* Si un usuario no autenticado intenta acceder a una ruta protegida, es redirigido automáticamente al formulario de Login (`/Login/Index`).
* El acceso al sistema requiere credenciales administrativas, las cuales generan un *ClaimsPrincipal* y una cookie cifrada que el servidor valida en cada petición. Los accesos inválidos (credenciales erróneas) son manejados devolviendo la vista con un mensaje de error y código 401/400 implícito en la lógica de negocio.

### Explicación de HTTPS y Riesgos Mitigados
El proyecto obliga el uso de conexiones seguras mediante el middleware `app.UseHttpsRedirection()`. 
* **Riesgo Mitigado:** El principal riesgo que mitiga HTTPS es el ataque **Man-in-the-Middle (MitM)**. Al usar TLS/SSL, todo el tráfico entre el navegador del estudiante o administrador y el servidor viaja cifrado. Si un atacante intercepta la red (por ejemplo, en un Wi-Fi público), no podrá leer los datos sensibles del formulario (como el RUT o los ingresos familiares) ni capturar la cookie de sesión del administrador, ya que solo verá caracteres incomprensibles.

---

## 🔌 Documentación de Endpoints y Flujo HTTP

El sistema cuenta con controladores MVC completos y una API RESTful interna. A continuación, el flujo de los endpoints principales:

### 1. API RESTful de Evaluación Preliminar
* **Endpoint:** `POST /api/becas/evaluar`
* **Uso:** Recibe un payload JSON con los datos del estudiante para simular su puntaje sin guardar en base de datos.
* **Códigos HTTP:**
  * `200 OK`: Cuando el JSON es válido y el cálculo se realiza con éxito, retorna el puntaje y resultado preliminar.
  * `400 Bad Request`: Si el JSON enviado está mal formado o faltan datos requeridos por el modelo.

### 2. Flujo CRUD (SolicitudBecasController)
* **GET `/SolicitudBecas` (Lista):** Retorna la vista `Index` con la lista completa de solicitudes. (Código `200 OK`).
* **GET `/SolicitudBecas/Details/{id}`:** Busca una solicitud específica. 
  * Retorna `200 OK` con la vista si existe.
  * Retorna `404 Not Found` si el ID no se encuentra en la base de datos.
* **POST `/SolicitudBecas/Create`:** Envía los datos del nuevo estudiante. 
  * Retorna `302 Found` (Redirección al Index) si la validación es correcta y se guarda en SQL Server.
  * Retorna `400 Bad Request` (Vista con errores) si el modelo es inválido (ej. RUT mal formado).
* **POST `/SolicitudBecas/Edit/{id}`:** Actualiza un registro.
  * Retorna `302 Found` si se actualiza con éxito.
  * Retorna `404 Not Found` si el ID no coincide, o `400 Bad Request` si los datos violan las *Data Annotations*.
* **POST `/SolicitudBecas/Delete/{id}`:** Elimina un registro físicamente de la base de datos y retorna `302 Found` (Redirección al Index).

---

## 📸 Evidencias de Entrega

A continuación, se presentan las evidencias gráficas del correcto funcionamiento del sistema según la rúbrica de evaluación:

### 1. Proyecto funcionando en Visual Studio
*(Captura de pantalla de Visual Studio con el proyecto en ejecución, mostrando la consola o el explorador de soluciones).*
<img width="2492" height="1220" alt="image" src="https://github.com/user-attachments/assets/5dd36f62-5c95-4ff2-8a75-48c3c4e1402e" />


### 2. Base de datos SQL Server creada
*(Captura de SQL Server Management Studio mostrando la base de datos `SistemaBecasDB`, la tabla y sus columnas).*
<img width="1990" height="952" alt="image" src="https://github.com/user-attachments/assets/18f48fe7-9c71-4119-82aa-e33e9c267dd9" />


### 3. Formulario de registro (Create)
*(Captura de la pantalla web llenando un estudiante nuevo).*
<img width="1078" height="1168" alt="image" src="https://github.com/user-attachments/assets/b0df670d-30f6-4096-b8dc-993134c8a5a7" />


### 4. Listado de solicitudes 
*(Captura de la tabla principal con Bootstrap y DataTables, mostrando varios registros).*
<img width="1918" height="930" alt="image" src="https://github.com/user-attachments/assets/9a1bc263-a41d-4a72-977b-9eebda1e4cdb" />


### 5. Edición o eliminación (Edit/Delete)
*(Captura de la pantalla de edición modificando un dato, y/o el modal/pantalla de confirmación de borrado).*
<img width="1022" height="1094" alt="image" src="https://github.com/user-attachments/assets/46a08eb9-5b00-4648-aff0-20dd4fda4a6d" />


### 6. Datos guardados en SQL Server
*(Captura de un `SELECT * FROM SolicitudesBeca` en SQL Server Management Studio demostrando que los datos de la web llegaron a la base de datos).*
<img width="1782" height="528" alt="image" src="https://github.com/user-attachments/assets/61eaf831-b3d5-4e56-afb0-9c403b57de99" />


### 7. Evidencia del cálculo automático del puntaje
*(Captura de un registro guardado donde se vea que el estudiante tiene notas/ingresos específicos y el sistema asignó automáticamente el Puntaje y el Estado sin que el usuario lo escribiera).*
<img width="1942" height="856" alt="image" src="https://github.com/user-attachments/assets/1569a410-01d5-4528-8ff5-b449a2bcd574" />


### 8. Pruebas del endpoint API (Postman)
*(Captura de Postman haciendo un POST a `https://localhost:7288/api/becas/evaluar` con el JSON de entrada y recibiendo el JSON de respuesta con el 200 OK).*
<img width="1898" height="1032" alt="image" src="https://github.com/user-attachments/assets/e1238c5f-d729-43d9-aab9-878dfddb6000" />

<img width="1894" height="1004" alt="image" src="https://github.com/user-attachments/assets/7ee57470-363b-4fed-b351-fec19ff4e37f" />

<img width="1906" height="1014" alt="image" src="https://github.com/user-attachments/assets/3a1603a1-f421-4c3c-b62d-1f3eba5b259e" />

---

## ⚙️ Instalación y Uso Rápido

1. Clonar este repositorio.
2. Abrir la solución `.sln` en Visual Studio.
3. Abrir la Consola del Administrador de Paquetes y ejecutar `Update-Database` para generar la estructura en SQL Server.
4. Ejecutar el proyecto (F5).
5. **Credenciales de Administrador:** Usuario: `admin` | Clave: `1234`
