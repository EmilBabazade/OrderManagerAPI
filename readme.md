# How To Run

1. Clone [OrderManager Backend](#)
2. Open it in Visual Studio
3. Build
4. Open NuGet Package Manager Console
    * run `update-database`
5. Run the project
6. Clone [OrderManager FrontEnd](#)
7. Open root directory in vscode
8. run `npm-install`
9. run `npm-start`
10. You can access the website Through `http://localhost:3000/`
    * Admin Email: `admin@admin.com` Password: `Qwerty@2`
    * Standart Email: `use@user.com` Password: `Qwerty@2`
    * Standart accounts can only view the orders, vaqons, etc but can not delete or create anything
    * Admin accounts can do anything

## Ports
Default port settings should work, but if they don't or you want to change them for some reaosn:
1. Make sure `BACKEND/Properities/launchSettings.json` `sslPort` value and `FRONTEND/app/core/port.service.js` controller return value are both the same
2. Make sure PORT value in `FRONTEND/package.json` `npm start` script and `BACKEND/appsettings.json` `FrontEndPort` value are the same
