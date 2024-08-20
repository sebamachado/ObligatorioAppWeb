UPDATE parametros SET valor = FORMAT(GETDATE(), 'dd/MM/yyyy HH:mm:ss') WHERE nombre = 'ultimoDeploy';
