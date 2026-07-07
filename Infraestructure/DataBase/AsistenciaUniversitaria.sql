CREATE DATABASE AsistenciaUniversitaria;
USE AsistenciaUniversitaria;

SET FOREIGN_KEY_CHECKS = 0;


CREATE TABLE IF NOT EXISTS `Asistencia` (
	`Id_Asistencia` INT AUTO_INCREMENT PRIMARY KEY,
	`Id_Sesion` INT NOT NULL,
	`Carnet_Usuario` VARCHAR(50) NOT NULL,
	`Estado` VARCHAR(20) CHECK(`Estado` IN ('Presente', 'Ausente', 'Justificado', 'Tarde')),
	`Hora_Registro` TIME DEFAULT (CURRENT_TIME),
	FOREIGN KEY(`Id_Sesion`) REFERENCES `Sesiones_Clase`(`Id_Sesion`)
);

CREATE TABLE IF NOT EXISTS `Auditoria_Log` (
	`Id_Log` INT AUTO_INCREMENT PRIMARY KEY,
	`Accion` VARCHAR(50),
	`Tabla_Afectada` VARCHAR(50),
	`Dato_Identificador` VARCHAR(255),
	`Fecha_Accion` TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS `Docentes` (
	`Carnet` VARCHAR(50) PRIMARY KEY,
	`Contrasena` VARCHAR(255) NOT NULL,
	`Primer_Nombre` VARCHAR(100) NOT NULL,
	`Segundo_Nombre` VARCHAR(100),
	`Primer_Apellido` VARCHAR(100) NOT NULL,
	`Segundo_Apellido` VARCHAR(100),
	`Codigo_Empleado` VARCHAR(50) NOT NULL UNIQUE,
	`Departamento` VARCHAR(100) NOT NULL,
	`Cantidad_Faltas` INT DEFAULT 0
);

CREATE TABLE IF NOT EXISTS `Empleados` (
	`Codigo_Empleado` VARCHAR(50) PRIMARY KEY,
	`Contrasena` VARCHAR(255) NOT NULL,
	`Primer_Nombre` VARCHAR(100) NOT NULL,
	`Segundo_Nombre` VARCHAR(100),
	`Primer_Apellido` VARCHAR(100) NOT NULL,
	`Segundo_Apellido` VARCHAR(100),
	`Departamento` VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS `Estudiantes` (
	`Carnet` VARCHAR(50) PRIMARY KEY,
	`Contrasena` VARCHAR(255) NOT NULL,
	`Primer_Nombre` VARCHAR(100) NOT NULL,
	`Segundo_Nombre` VARCHAR(100),
	`Primer_Apellido` VARCHAR(100) NOT NULL,
	`Segundo_Apellido` VARCHAR(100),
	`Carrera` VARCHAR(100) NOT NULL,
	`Anio_Ingreso` INT NOT NULL,
	`Cantidad_Faltas` INT DEFAULT 0
);

CREATE TABLE IF NOT EXISTS `Estudiantes_Sancionados` (
	`Carnet_Estudiante` VARCHAR(50) PRIMARY KEY,
	`Cantidad_Faltas_Registradas` INT,
	`Fecha_Sancion` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY(`Carnet_Estudiante`) REFERENCES `Estudiantes`(`Carnet`)
);

CREATE TABLE IF NOT EXISTS `Grupos` (
	`Codigo_Grupo` VARCHAR(50) PRIMARY KEY,
	`Id_Asignatura` VARCHAR(100) NOT NULL,
	`Carnet_Docente` VARCHAR(50),
	`Seccion` VARCHAR(50) NOT NULL,
	FOREIGN KEY(`Carnet_Docente`) REFERENCES `Docentes`(`Carnet`)
);

CREATE TABLE IF NOT EXISTS `Matriculas` (
	`Id_Matricula` INT AUTO_INCREMENT PRIMARY KEY,
	`Carnet_Estudiante` VARCHAR(50) NOT NULL,
	`Codigo_Grupo` VARCHAR(50) NOT NULL,
	`Fecha_Registro` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY(`Carnet_Estudiante`) REFERENCES `Estudiantes`(`Carnet`),
	FOREIGN KEY(`Codigo_Grupo`) REFERENCES `Grupos`(`Codigo_Grupo`)
);

CREATE TABLE IF NOT EXISTS `Sesiones_Clase` (
	`Id_Sesion` INT AUTO_INCREMENT PRIMARY KEY,
	`Codigo_Grupo` VARCHAR(50) NOT NULL,
	`Fecha` DATE NOT NULL,
	`Tema_Impartido` VARCHAR(255),
	`Hora_Inicio` TIME,
	`Hora_Fin` TIME,
	FOREIGN KEY(`Codigo_Grupo`) REFERENCES `Grupos`(`Codigo_Grupo`)
);



INSERT INTO `Asistencia` (`Id_Asistencia`,`Id_Sesion`,`Carnet_Usuario`,`Estado`,`Hora_Registro`) VALUES 
(1,1,'240871','Presente','09:11:36');

INSERT INTO `Auditoria_Log` (`Id_Log`,`Accion`,`Tabla_Afectada`,`Dato_Identificador`,`Fecha_Accion`) VALUES 
(1,'INSERT','Estudiantes','Carnet: 240871','2026-07-01 09:02:43'),
(2,'INSERT','Estudiantes','Carnet: 240909','2026-07-01 09:02:43'),
(3,'INSERT','Estudiantes','Carnet: 240908','2026-07-01 09:02:43'),
(4,'INSERT','Estudiantes','Carnet: REGISTRO_BORRAR','2026-07-01 09:02:43');

INSERT INTO `Docentes` (`Carnet`,`Contrasena`,`Primer_Nombre`,`Segundo_Nombre`,`Primer_Apellido`,`Segundo_Apellido`,`Codigo_Empleado`,`Departamento`,`Cantidad_Faltas`) VALUES 
('D-240101','VidalDeidad123','Vidal','Jose','Herrera','Gutierrez','D001','Ingenieria en Sistema',0),
('D-240102','ElTerrorDeLosEstudiantes123','David','de Jesus','Lovo','Moreira','D002','Ingenieria en Sistema',0),
('D-240103','MentalidadDeTiburon','Eduardo','Alfonso','Hernandez','Alvarado','D003','Finanzas',0);

INSERT INTO `Estudiantes` (`Carnet`,`Contrasena`,`Primer_Nombre`,`Segundo_Nombre`,`Primer_Apellido`,`Segundo_Apellido`,`Carrera`,`Anio_Ingreso`,`Cantidad_Faltas`) VALUES 
('240871','NeuvileteDeidad','Gustavo','Adolfo','Davila','Maradiaga','Ingeniería en Sistemas',2024,0),
('240909','ArlequinoPisame','Victor','Miguel','Romero','Espinoza','Ingeniería en Sistemas',2024,0),
('240908','NeuvilleteYaNoPega','Jizmeth','Raul','Lainez','Chavez','Ingeniería en Sistemas',2024,0),
('REGISTRO_BORRAR','0000','Prueba',NULL,'Eliminar',NULL,'Ninguna',2026,0);

INSERT INTO `Grupos` (`Codigo_Grupo`,`Id_Asignatura`,`Carnet_Docente`,`Seccion`) VALUES 
('MI08C','Programación Orientada a Objetos','D-240101','L12');

INSERT INTO `Matriculas` (`Id_Matricula`,`Carnet_Estudiante`,`Codigo_Grupo`,`Fecha_Registro`) VALUES 
(1,'240871','MI08C','2026-07-01 09:02:43'),
(2,'240909','MI08C','2026-07-01 09:02:43'),
(3,'240908','MI08C','2026-07-01 09:02:43'),
(4,'240871','MI08C','2026-07-01 09:11:36'),
(5,'240909','MI08C','2026-07-01 09:11:36'),
(6,'240908','MI08C','2026-07-01 09:11:36');

INSERT INTO `Sesiones_Clase` (`Id_Sesion`,`Codigo_Grupo`,`Fecha`,`Tema_Impartido`,`Hora_Inicio`,`Hora_Fin`) VALUES 
(1,'MI08C','2026-06-01','Programacion con Vidal Deidad',NULL,NULL),
(2,'MI08C','2026-06-02','Follada con Lovo',NULL,NULL),
(3,'MI08C','2026-06-03','¿Travesti feo o Chica Linda? con Eduardo Alfonso de Contabilidad',NULL,NULL);



CREATE VIEW vw_PerfilCompletoEstudiante AS
SELECT 
    E.Carnet,
    E.Contrasena,
    CONCAT(E.Primer_Nombre, ' ', E.Primer_Apellido) AS Nombre_Estudiante,
    E.Carrera,
    G.Codigo_Grupo,
    G.Id_Asignatura AS Nombre_Clase,
    G.Seccion,
    CONCAT(D.Primer_Nombre, ' ', D.Primer_Apellido) AS Profesor_Imparte
FROM Estudiantes E
JOIN Matriculas M ON E.Carnet = M.Carnet_Estudiante
JOIN Grupos G ON M.Codigo_Grupo = G.Codigo_Grupo
LEFT JOIN Docentes D ON G.Carnet_Docente = D.Carnet;

CREATE VIEW vw_ReporteAsistencia AS
SELECT 
    A.Id_Asistencia, S.Fecha, G.Codigo_Grupo, G.Seccion, A.Carnet_Usuario,
    CONCAT(E.Primer_Nombre, ' ', E.Primer_Apellido) AS Nombre_Estudiante,
    A.Estado, A.Hora_Registro
FROM Asistencia A
JOIN Sesiones_Clase S ON A.Id_Sesion = S.Id_Sesion
JOIN Grupos G ON S.Codigo_Grupo = G.Codigo_Grupo
JOIN Estudiantes E ON A.Carnet_Usuario = E.Carnet;



DELIMITER $$

CREATE TRIGGER trg_ActualizarFaltasEstudiante
AFTER INSERT ON Asistencia
FOR EACH ROW
BEGIN
    IF NEW.Estado = 'Ausente' THEN
        UPDATE Estudiantes
        SET Cantidad_Faltas = Cantidad_Faltas + 1
        WHERE Carnet = NEW.Carnet_Usuario;
    END IF;
END$$

CREATE TRIGGER trg_Auditoria_Delete_Estudiante
AFTER DELETE ON Estudiantes
FOR EACH ROW
BEGIN
    INSERT INTO Auditoria_Log (Accion, Tabla_Afectada, Dato_Identificador)
    VALUES ('DELETE', 'Estudiantes', CONCAT('Carnet: ', OLD.Carnet));
END$$

CREATE TRIGGER trg_Auditoria_Insert_Estudiante
AFTER INSERT ON Estudiantes
FOR EACH ROW
BEGIN
    INSERT INTO Auditoria_Log (Accion, Tabla_Afectada, Dato_Identificador)
    VALUES ('INSERT', 'Estudiantes', CONCAT('Carnet: ', NEW.Carnet));
END$$

CREATE TRIGGER trg_VerificarSancionEstudiante
AFTER UPDATE ON Estudiantes
FOR EACH ROW
BEGIN
    IF NEW.Cantidad_Faltas >= 4 THEN
        REPLACE INTO Estudiantes_Sancionados (Carnet_Estudiante, Cantidad_Faltas_Registradas)
        VALUES (NEW.Carnet, NEW.Cantidad_Faltas);
    END IF;
END$$

DELIMITER ;

