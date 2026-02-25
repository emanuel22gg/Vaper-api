---
description: Cómo ejecutar y probar la API localmente
---

Para ejecutar la API de Vaper en tu máquina local, sigue estos pasos:

### Opción 1: Usando Visual Studio (Recomendado)
1. Abre el archivo de solución `Vaper_Api.sln`.
2. Asegúrate de que el proyecto de inicio sea `Vaper_Api`.
3. Presiona la tecla **F5** o haz clic en el botón **Play (flecha verde)** que dice "http" o "IIS Express".
4. Se abrirá automáticamente tu navegador en la página de **Swagger** (donde podrás probar todos los métodos).

### Opción 2: Usando la Terminal (PowerShell o CMD)
1. Abre una terminal en la carpeta raíz del proyecto (`c:\Users\USER\source\repos\Vaper-api\Vaper_Api`).
2. Ejecuta el siguiente comando:
   ```powershell
   dotnet run
   ```
3. Una vez que diga `Now listening on: http://localhost:5081`, abre tu navegador y ve a:
   [http://localhost:5081/swagger](http://localhost:5081/swagger)

### Cómo probar el nuevo método de Compras
1. En Swagger, busca el endpoint `POST /api/Compras`.
2. Haz clic en **Try it out**.
3. Pega un JSON que incluya la lista de detalles, como este:
   ```json
   {
     "numeroCompra": "PRUEBA-001",
     "fechaCompra": "2024-03-24T17:35:00",
     "proveedorId": 1,
     "detalleCompras": [
       {
         "productoId": 1,
         "cantidad": 5,
         "precioUnitario": 10.0,
         "subtotal": 50.0
       }
     ]
   }
   ```
4. Haz clic en **Execute** y verifica que la respuesta sea un código `201 Created`.
