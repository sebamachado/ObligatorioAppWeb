-- Obtén la fecha y hora actual en UTC
DECLARE @CurrentUTCDateTime DATETIME = GETUTCDATE();

-- Ajusta la fecha y hora actual al huso horario de Montevideo (UTC-3) y actualiza la columna
UPDATE parametros
SET valor = FORMAT(SWITCHOFFSET(CONVERT(DATETIMEOFFSET, @CurrentUTCDateTime), '-03:00'), 'dd/MM/yyyy HH:mm:ss')
WHERE nombre = 'ultimoDeploy';
