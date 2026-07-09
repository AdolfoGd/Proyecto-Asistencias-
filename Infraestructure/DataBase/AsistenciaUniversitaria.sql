CREATE DATABASE AsistenciaUniversitaria;
USE AsistenciaUniversitaria;

SET FOREIGN_KEY_CHECKS = 0;

CREATE TABLE IF NOT EXISTS `Asistencia` (
	`Id_Asistencia` INT AUTO_INCREMENT PRIMARY KEY,
	`Id_Sesion` INT NOT NULL,
	`Carnet_Usuario` VARCHAR(50) NOT NULL,
	`Estado` VARCHAR(20) CHECK(`Estado` IN ('Presente', 'Ausente', 'Justificado', 'Tarde')),
	`Hora_Registro` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY(`Id_Sesion`) REFERENCES `Sesiones_Clase`(`Id_Sesion`)
);

-- CRUD de ASISTENCIA
DELIMITER //

CREATE PROCEDURE sp_InsertarAsistencia(
    IN p_id_sesion INT, 
    IN p_carnet VARCHAR(50), 
    IN p_estado VARCHAR(20)
)
BEGIN
    INSERT INTO Asistencia (Id_Sesion, Carnet_Usuario, Estado) 
    VALUES (p_id_sesion, p_carnet, p_estado);
END //

CREATE PROCEDURE sp_LeerAsistencias()
BEGIN
    SELECT * FROM Asistencia;
END //

CREATE PROCEDURE sp_ActualizarAsistencia(
    IN p_id INT, 
    IN p_estado VARCHAR(20)
)
BEGIN
    UPDATE Asistencia 
    SET Estado = p_estado, Hora_Registro = CURRENT_TIME() 
    WHERE Id_Asistencia = p_id;
END //

CREATE PROCEDURE sp_EliminarAsistencia(IN p_id INT)
BEGIN
    DELETE FROM Asistencia WHERE Id_Asistencia = p_id;
END //

CREATE TABLE IF NOT EXISTS `Auditoria_Log` (
	`Id_Log` INT AUTO_INCREMENT PRIMARY KEY,
	`Accion` VARCHAR(50),
	`Tabla_Afectada` VARCHAR(50),
	`Dato_Identificador` VARCHAR(255),
	`Fecha_Accion` TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- CRUD de AUDITORIA_LOG
CREATE PROCEDURE sp_InsertarLog(
    IN p_accion VARCHAR(50), 
    IN p_tabla VARCHAR(50), 
    IN p_identificador VARCHAR(255)
)
BEGIN
    INSERT INTO Auditoria_Log (Accion, Tabla_Afectada, Dato_Identificador) 
    VALUES (p_accion, p_tabla, p_identificador);
END //

CREATE PROCEDURE sp_LeerLogs()
BEGIN
    SELECT * FROM Auditoria_Log ORDER BY Fecha_Accion DESC;
END //

CREATE PROCEDURE sp_EliminarLog(IN p_id INT)
BEGIN
    DELETE FROM Auditoria_Log WHERE Id_Log = p_id;
END //


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

-- CRUD de DOCENTES
CREATE PROCEDURE sp_InsertarDocente(
    IN p_carnet VARCHAR(50), IN p_pass VARCHAR(255), IN p_n1 VARCHAR(100), IN p_n2 VARCHAR(100),
    IN p_app1 VARCHAR(100), IN p_app2 VARCHAR(100), IN p_cod VARCHAR(50), IN p_dept VARCHAR(100)
)
BEGIN
    INSERT INTO Docentes (Carnet, Contrasena, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Codigo_Empleado, Departamento) 
    VALUES (p_carnet, p_pass, p_n1, p_n2, p_app1, p_app2, p_cod, p_dept);
END //

CREATE PROCEDURE sp_LeerDocentes()
BEGIN
    SELECT * FROM Docentes;
END //

CREATE PROCEDURE sp_ActualizarDocente(
    IN p_carnet VARCHAR(50), IN p_pass VARCHAR(255), IN p_n1 VARCHAR(100), IN p_n2 VARCHAR(100),
    IN p_app1 VARCHAR(100), IN p_app2 VARCHAR(100), IN p_dept VARCHAR(100), IN p_faltas INT
)
BEGIN
    UPDATE Docentes 
    SET Contrasena = p_pass, Primer_Nombre = p_n1, Segundo_Nombre = p_n2, 
        Primer_Apellido = p_app1, Segundo_Apellido = p_app2, Departamento = p_dept, Cantidad_Faltas = p_faltas
    WHERE Carnet = p_carnet;
END //

CREATE PROCEDURE sp_EliminarDocente(IN p_carnet VARCHAR(50))
BEGIN
    DELETE FROM Docentes WHERE Carnet = p_carnet;
END //


CREATE TABLE IF NOT EXISTS `Empleados` (
	`Codigo_Empleado` VARCHAR(50) PRIMARY KEY,
	`Contrasena` VARCHAR(255) NOT NULL,
	`Primer_Nombre` VARCHAR(100) NOT NULL,
	`Segundo_Nombre` VARCHAR(100),
	`Primer_Apellido` VARCHAR(100) NOT NULL,
	`Segundo_Apellido` VARCHAR(100),
	`Departamento` VARCHAR(100) NOT NULL
);

-- CRUD de EMPLEADOS
CREATE PROCEDURE sp_InsertarEmpleado(
    IN p_cod VARCHAR(50), IN p_pass VARCHAR(255), IN p_n1 VARCHAR(100), 
    IN p_n2 VARCHAR(100), IN p_app1 VARCHAR(100), IN p_app2 VARCHAR(100), IN p_dept VARCHAR(100)
)
BEGIN
    INSERT INTO Empleados (Codigo_Empleado, Contrasena, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Departamento) 
    VALUES (p_cod, p_pass, p_n1, p_n2, p_app1, p_app2, p_dept);
END //

CREATE PROCEDURE sp_LeerEmpleados()
BEGIN
    SELECT * FROM Empleados;
END //

CREATE PROCEDURE sp_ActualizarEmpleado(
    IN p_cod VARCHAR(50), IN p_pass VARCHAR(255), IN p_n1 VARCHAR(100), 
    IN p_n2 VARCHAR(100), IN p_app1 VARCHAR(100), IN p_app2 VARCHAR(100), IN p_dept VARCHAR(100)
)
BEGIN
    UPDATE Empleados 
    SET Contrasena = p_pass, Primer_Nombre = p_n1, Segundo_Nombre = p_n2, 
        Primer_Apellido = p_app1, Segundo_Apellido = p_app2, Departamento = p_dept
    WHERE Codigo_Empleado = p_cod;
END //

CREATE PROCEDURE sp_EliminarEmpleado(IN p_cod VARCHAR(50))
BEGIN
    DELETE FROM Empleados WHERE Codigo_Empleado = p_cod;
END //


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

-- CRUD de ESTUDIANTES
CREATE PROCEDURE sp_InsertarEstudiante(
    IN p_carnet VARCHAR(50), IN p_pass VARCHAR(255), IN p_n1 VARCHAR(100), IN p_n2 VARCHAR(100),
    IN p_app1 VARCHAR(100), IN p_app2 VARCHAR(100), IN p_carrera VARCHAR(100), IN p_anio INT
)
BEGIN
    INSERT INTO Estudiantes (Carnet, Contrasena, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Carrera, Anio_Ingreso) 
    VALUES (p_carnet, p_pass, p_n1, p_n2, p_app1, p_app2, p_carrera, p_anio);
END //

CREATE PROCEDURE sp_LeerEstudiantes()
BEGIN
    SELECT * FROM Estudiantes;
END //

CREATE PROCEDURE sp_ActualizarEstudiante(
    IN p_carnet VARCHAR(50), IN p_pass VARCHAR(255), IN p_n1 VARCHAR(100), IN p_n2 VARCHAR(100),
    IN p_app1 VARCHAR(100), IN p_app2 VARCHAR(100), IN p_carrera VARCHAR(100), IN p_faltas INT
)
BEGIN
    UPDATE Estudiantes 
    SET Contrasena = p_pass, Primer_Nombre = p_n1, Segundo_Nombre = p_n2, 
        Primer_Apellido = p_app1, Segundo_Apellido = p_app2, Carrera = p_carrera, Cantidad_Faltas = p_faltas
    WHERE Carnet = p_carnet;
END //

CREATE PROCEDURE sp_EliminarEstudiante(IN p_carnet VARCHAR(50))
BEGIN
    DELETE FROM Estudiantes WHERE Carnet = p_carnet;
END //


CREATE TABLE IF NOT EXISTS `Estudiantes_Sancionados` (
	`Carnet_Estudiante` VARCHAR(50) PRIMARY KEY,
	`Cantidad_Faltas_Registradas` INT,
	`Fecha_Sancion` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY(`Carnet_Estudiante`) REFERENCES `Estudiantes`(`Carnet`)
);

-- CRUD de ESTUDIANTES_SANCIONADOS
CREATE PROCEDURE sp_InsertarSancionado(
    IN p_carnet VARCHAR(50), 
    IN p_faltas INT
)
BEGIN
    REPLACE INTO Estudiantes_Sancionados (Carnet_Estudiante, Cantidad_Faltas_Registradas) 
    VALUES (p_carnet, p_faltas);
END //

CREATE PROCEDURE sp_LeerSancionados()
BEGIN
    SELECT * FROM Estudiantes_Sancionados;
END //

CREATE PROCEDURE sp_EliminarSancionado(IN p_carnet VARCHAR(50))
BEGIN
    DELETE FROM Estudiantes_Sancionados WHERE Carnet_Estudiante = p_carnet;
END //


CREATE TABLE IF NOT EXISTS `Grupos` (
	`Codigo_Grupo` VARCHAR(50) PRIMARY KEY,
	`Id_Asignatura` VARCHAR(100) NOT NULL,
	`Carnet_Docente` VARCHAR(50),
	`Seccion` VARCHAR(50) NOT NULL,
	FOREIGN KEY(`Carnet_Docente`) REFERENCES `Docentes`(`Carnet`)
);

-- CRUD de GRUPOS
CREATE PROCEDURE sp_InsertarGrupo(
    IN p_cod VARCHAR(50), 
    IN p_asignatura VARCHAR(100), 
    IN p_docente VARCHAR(50), 
    IN p_seccion VARCHAR(50)
)
BEGIN
    INSERT INTO Grupos (Codigo_Grupo, Id_Asignatura, Carnet_Docente, Seccion) 
    VALUES (p_cod, p_asignatura, p_docente, p_seccion);
END //

CREATE PROCEDURE sp_LeerGrupos()
BEGIN
    SELECT * FROM Grupos;
END //

CREATE PROCEDURE sp_ActualizarGrupo(
    IN p_cod VARCHAR(50), 
    IN p_asignatura VARCHAR(100), 
    IN p_docente VARCHAR(50), 
    IN p_seccion VARCHAR(50)
)
BEGIN
    UPDATE Grupos 
    SET Id_Asignatura = p_asignatura, Carnet_Docente = p_docente, Seccion = p_seccion 
    WHERE Codigo_Grupo = p_cod;
END //

CREATE PROCEDURE sp_EliminarGrupo(IN p_cod VARCHAR(50))
BEGIN
    DELETE FROM Grupos WHERE Codigo_Grupo = p_cod;
END //

CREATE TABLE IF NOT EXISTS `Matriculas` (
	`Id_Matricula` INT AUTO_INCREMENT PRIMARY KEY,
	`Carnet_Estudiante` VARCHAR(50) NOT NULL,
	`Codigo_Grupo` VARCHAR(50) NOT NULL,
	`Fecha_Registro` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY(`Carnet_Estudiante`) REFERENCES `Estudiantes`(`Carnet`),
	FOREIGN KEY(`Codigo_Grupo`) REFERENCES `Grupos`(`Codigo_Grupo`)
);

-- CRUD de MATRICULAS
CREATE PROCEDURE sp_InsertarMatricula(
    IN p_carnet VARCHAR(50), 
    IN p_grupo VARCHAR(50)
)
BEGIN
    INSERT INTO Matriculas (Carnet_Estudiante, Codigo_Grupo) 
    VALUES (p_carnet, p_grupo);
END //

CREATE PROCEDURE sp_LeerMatriculas()
BEGIN
    SELECT * FROM Matriculas;
END //

CREATE PROCEDURE sp_ActualizarMatricula(
    IN p_id INT, 
    IN p_carnet VARCHAR(50), 
    IN p_grupo VARCHAR(50)
)
BEGIN
    UPDATE Matriculas 
    SET Carnet_Estudiante = p_carnet, Codigo_Grupo = p_grupo 
    WHERE Id_Matricula = p_id;
END //

CREATE PROCEDURE sp_EliminarMatricula(IN p_id INT)
BEGIN
    DELETE FROM Matriculas WHERE Id_Matricula = p_id;
END //

CREATE TABLE IF NOT EXISTS `Sesiones_Clase` (
	`Id_Sesion` INT AUTO_INCREMENT PRIMARY KEY,
	`Codigo_Grupo` VARCHAR(50) NOT NULL,
	`Fecha` DATE NOT NULL,
	`Tema_Impartido` VARCHAR(255),
	`Hora_Inicio` TIME,
	`Hora_Fin` TIME,
	FOREIGN KEY(`Codigo_Grupo`) REFERENCES `Grupos`(`Codigo_Grupo`)
);

-- CRUDS de Sesiones de Clases
CREATE PROCEDURE sp_InsertarSesion(
    IN p_grupo VARCHAR(50), IN p_fecha DATE, 
    IN p_tema VARCHAR(255), IN p_inicio TIME, IN p_fin TIME
)
BEGIN
    INSERT INTO Sesiones_Clase (Codigo_Grupo, Fecha, Tema_Impartido, Hora_Inicio, Hora_Fin) 
    VALUES (p_grupo, p_fecha, p_tema, p_inicio, p_fin);
END //

CREATE PROCEDURE sp_LeerSesiones()
BEGIN
    SELECT * FROM Sesiones_Clase;
END //

CREATE PROCEDURE sp_ActualizarSesion(
    IN p_id INT, IN p_fecha DATE, 
    IN p_tema VARCHAR(255), IN p_inicio TIME, IN p_fin TIME
)
BEGIN
    UPDATE Sesiones_Clase 
    SET Fecha = p_fecha, Tema_Impartido = p_tema, Hora_Inicio = p_inicio, Hora_Fin = p_fin 
    WHERE Id_Sesion = p_id;
END //

CREATE PROCEDURE sp_EliminarSesion(IN p_id INT)
BEGIN
    DELETE FROM Sesiones_Clase WHERE Id_Sesion = p_id;
END //
DELIMITER ;

-- Insercion de Datos

INSERT INTO `Asistencia` (`Id_Asistencia`,`Id_Sesion`,`Carnet_Usuario`,`Estado`,`Hora_Registro`) VALUES 
(1,1,'240871','Presente','2026/07/09 09:11:36');

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
-- Creación de Vistas
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

-- Creacion de Triggers

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
