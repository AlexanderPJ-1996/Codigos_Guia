-- Crear base de datos
--

-- MySQL/MariaDB
CREATE DATABASE 'Nombre_BasedeDatos' COLLATE 'utf8mb4_spanish_ci'; -- Definir COLLATE de la base de datos

-- Crear Tablas
--

-- Microsoft SQL Server
CREATE TABLE [dbo].['Nombre_Tabla']
(
	-- Definir PRIMARY KEY que inicia en 1, y autoincrementa en 1
	['Nombre_Columna'] BIGINT NOT NULL PRIMARY KEY IDENTITY(1, 1)
);

-- MySQL/MariaDB
CREATE TABLE IF NOT EXISTS 'Nombre_Tabla' ( -- Definir condición para crear tabla si no existe
	-- Definir autoincremento
	'Nombre_Columna' BIGINT NOT NULL AUTO_INCREMENT, 
	-- Definir PRIMARY KEY
	PRIMARY KEY ('Nombre_Columna')
)
COLLATE='utf8mb4_spanish_ci' -- Definir COLLATE de la tabla
;

-- SQLite
CREATE TABLE IF NOT EXISTS 'Nombre_Tabla' ( -- Definir condición para crear tabla si no existe
	-- Definir PRIMARY KEY que autoincrementa
	'Nombre_Columna' INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT
);

-- Crear vistas
--

-- Microsoft SQL Server
CREATE VIEW [dbo].['Nombre_Vista'] AS 
SELECT * FROM 'Nombre_Tabla';

-- MySQL/MariaDB, SQLite con condición para crear vista si no existe
CREATE VIEW IF NOT EXISTS 'Nombre_Vista' AS 
SELECT * FROM 'Nombre_Tabla';

-- CRUD con tablas
--

-- SELECT: mostrar registros
SELECT * FROM 'Nombre_Tabla';
-- INSERT INTO: agregar valores nuevos
INSERT INTO 'Nombre_Tabla' ('Nombre_Columna') VALUES ('Valor_Columna');
-- UPDATE: editar valores
UPDATE 'Nombre_Tabla' SET 'Nombre_Columna' = 'Valor_Columna';
-- DELETE: eliminar datos
DELETE FROM 'Nombre_Tabla';
-- Filtrar datos para SELECT, UPDATE y DELETE
WHERE ('Nombre_Columna' = 'Valor_Columna');
-- ORDER BY: ordenar registros de forma ascendente o descendente
ORDER BY 'Nombre_Columna' ASC/DESC;

-- Funciones
--

-- COUNT(): contar y retornar n° de registros de una tabla
SELECT COUNT(*) FROM 'Nombre_Tabla';
-- SUM(): retornar suma de todos los valores numéricos de una columna
SELECT SUM('Nombre_Columna') AS 'Nombre_Columna' FROM 'Nombre_Tabla';
-- MAX(): retornar valor numérico más alto de una columna
SELECT MAX('Nombre_Columna') AS 'Nombre_Columna' FROM 'Nombre_Tabla';
-- MIN(): retornar valor numérico más bajo de una columna
SELECT MIN('Nombre_Columna') AS 'Nombre_Columna' FROM 'Nombre_Tabla';
-- AVG(): retornar valor del promedio de una columna
SELECT AVG('Nombre_Columna') AS 'Nombre_Columna' FROM 'Nombre_Tabla';

-- REPLACE(): retornar columna con caracteres reemplazados/removidos
SELECT REPLACE('Nombre_Columna', ' ', '') AS 'Nombre_Columna' FROM 'Nombre_Tabla';
-- CAST(): convertir una columna de un tipo en otro tipo de datos
SELECT CAST('Nombre_Columna' AS 'Tipo_Columna'('Longitud_Columna')) AS 'Nombre_Columna' FROM 'Nombre_Tabla';
-- CONCAT(): concatenar (unir) columnas
SELECT CONCAT('Nombre_Columna1', 'Nombre_Columna2') AS 'Nombre_Columna' FROM 'Nombre_Tabla';
-- CONCAT(): concatenar (unir) columnas en SQLite
SELECT 'Nombre_Columna1' || 'Nombre_Columna2' AS 'Nombre_Columna' FROM 'Nombre_Tabla';

-- Agrupar una columna en base a otra columna
SELECT 'Nombre_Columna1', COUNT('Nombre_Columna2') AS 'Nombre_Columna' FROM 'Nombre_Tabla' GROUP BY 'Nombre_Columna1';

-- Agrupar columnas y separarlas con comas en una unica columna

-- Microsoft SQL Server
SELECT 'Nombre_Columna1', STRING_AGG('Nombre_Columna2', ', ') AS 'Nombre_Columna' 
FROM 'Nombre_Tabla' 
GROUP BY 'Nombre_Columna1';
-- MySQL/MariaDB
SELECT 'Nombre_Columna1', GROUP_CONCAT('Nombre_Columna2' SEPARATOR ', ') AS 'Nombre_Columna' 
FROM 'Nombre_Tabla' 
GROUP BY 'Nombre_Columna1';
