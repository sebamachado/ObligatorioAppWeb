UPDATE parametros
SET valor = FORMAT(SWITCHOFFSET(CONVERT(DATETIMEOFFSET, GETDATE()) AT TIME ZONE 'UTC', '-03:00'), 'dd/MM/yyyy HH:mm:ss')
WHERE nombre = 'ultimoDeploy';
