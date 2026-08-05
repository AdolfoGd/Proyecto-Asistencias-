CREATE DATABASE AsistenciaUniversitaria;
USE AsistenciaUniversitaria;

SET FOREIGN_KEY_CHECKS = 0;

CREATE TABLE IF NOT EXISTS `Asistencia` (
	`Id_Asistencia` INT AUTO_INCREMENT PRIMARY KEY,
	`Id_Sesion` INT NOT NULL,
	`Carnet_Usuario` VARCHAR(50) NOT NULL,
	`Estado` VARCHAR(20) CHECK(`Estado` IN ('Presente', 'Ausente', 'Justificado', 'Tarde')),
	`Hora_Registro` TIME DEFAULT NULL,
	FOREIGN KEY(`Id_Sesion`) REFERENCES `Sesiones_Clase`(`Id_Sesion`)
);

-- CRUD de ASISTENCIA

DELIMITER //

CREATE PROCEDURE sp_InsertarAsistencia(
    IN idSesion INT,
    IN carnet VARCHAR(50),
    IN estado VARCHAR(20)
)
BEGIN
    INSERT INTO Asistencia (Id_Sesion, Carnet_Usuario, Estado, Hora_Registro) 
    VALUES (idSesion, carnet, estado, CURRENT_TIME());
END //

CREATE PROCEDURE sp_LeerAsistencias()
BEGIN
    SELECT 
        Id_Asistencia AS IdAsistencia,
        Id_Sesion AS IdSesion,
        Carnet_Usuario AS Carnet,
        Estado AS Estado,
        Hora_Registro AS Hora_Registro
    FROM Asistencia;
END //

CREATE PROCEDURE sp_LeerAsistenciaPorSesion(
    IN idSesion INT
)
BEGIN
    SELECT 
        Id_Asistencia AS IdAsistencia,
        Id_Sesion AS IdSesion,
        Carnet_Usuario AS Carnet,
        Estado AS Estado,
        Hora_Registro AS Hora_Registro
    FROM Asistencia
    WHERE Id_Sesion = idSesion;
END //

CREATE PROCEDURE sp_ActualizarAsistencia(
    IN idAsistencia INT,
    IN estado VARCHAR(20)
)
BEGIN
    UPDATE Asistencia 
    SET Estado = estado, 
        Hora_Registro = CURRENT_TIME() 
    WHERE Id_Asistencia = idAsistencia;
END //

CREATE PROCEDURE sp_EliminarAsistencia(
    IN id INT
)
BEGIN
    DELETE FROM Asistencia WHERE Id_Asistencia = id;
END //

DELIMITER ;

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

-- CRUD de DOCENTES
DELIMITER //

CREATE PROCEDURE sp_InsertarDocente(
    IN carnet VARCHAR(50),
    IN contrasena VARCHAR(255),
    IN primerNombre VARCHAR(100),
    IN segundoNombre VARCHAR(100),
    IN primerApellido VARCHAR(100),
    IN segundoApellido VARCHAR(100),
    IN codigoEmpleado VARCHAR(50),
    IN departamento VARCHAR(100)
)
BEGIN
    INSERT INTO Docentes (Carnet, Contrasena, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Codigo_Empleado, Departamento) 
    VALUES (carnet, contrasena, primerNombre, segundoNombre, primerApellido, segundoApellido, codigoEmpleado, departamento);
END //

CREATE PROCEDURE sp_LeerDocentes()
BEGIN
    SELECT 
        Carnet AS Carnet,
        Contrasena AS Contraseña,
        Primer_Nombre AS PrimerNombre,
        Segundo_Nombre AS SegundoNombre,
        Primer_Apellido AS PrimerApellido,
        Segundo_Apellido AS SegundoApellido,
        Codigo_Empleado AS CodigoEmpleado,
        Departamento AS Departamento,
        Cantidad_Faltas AS CantidadFaltas
    FROM Docentes;
END //

CREATE PROCEDURE sp_LeerDocentesPorDepartamento(
    IN departamento VARCHAR(100)
)
BEGIN
    SELECT 
        Carnet AS Carnet,
        Contrasena AS Contraseña,
        Primer_Nombre AS PrimerNombre,
        Segundo_Nombre AS SegundoNombre,
        Primer_Apellido AS PrimerApellido,
        Segundo_Apellido AS SegundoApellido,
        Codigo_Empleado AS CodigoEmpleado,
        Departamento AS Departamento,
        Cantidad_Faltas AS CantidadFaltas
    FROM Docentes
    WHERE Departamento = departamento;
END //

CREATE PROCEDURE sp_ActualizarDocente(
    IN carnet VARCHAR(50),
    IN contrasena VARCHAR(255),
    IN primerNombre VARCHAR(100),
    IN segundoNombre VARCHAR(100),
    IN primerApellido VARCHAR(100),
    IN segundoApellido VARCHAR(100),
    IN departamento VARCHAR(100),
    IN cantidadFaltas INT
)
BEGIN
    UPDATE Docentes 
    SET Contrasena = contrasena, 
        Primer_Nombre = primerNombre, 
        Segundo_Nombre = segundoNombre, 
        Primer_Apellido = primerApellido, 
        Segundo_Apellido = segundoApellido, 
        Departamento = departamento, 
        Cantidad_Faltas = cantidadFaltas
    WHERE Carnet = carnet;
END //

CREATE PROCEDURE sp_EliminarDocente(
    IN id VARCHAR(50)
)
BEGIN
    DELETE FROM Docentes WHERE Carnet = id;
END //

DELIMITER ;

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
DELIMITER //

CREATE PROCEDURE sp_InsertarEmpleado(
    IN codigoEmpleado VARCHAR(50),
    IN contrasena VARCHAR(255),
    IN primerNombre VARCHAR(100),
    IN segundoNombre VARCHAR(100),
    IN primerApellido VARCHAR(100),
    IN segundoApellido VARCHAR(100),
    IN departamento VARCHAR(100)
)
BEGIN
    INSERT INTO Empleados (Codigo_Empleado, Contrasena, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Departamento) 
    VALUES (codigoEmpleado, contrasena, primerNombre, segundoNombre, primerApellido, segundoApellido, departamento);
END //

CREATE PROCEDURE sp_LeerEmpleados()
BEGIN
    SELECT 
        Codigo_Empleado AS CodigoEmpleado,
        Contrasena AS Contraseña,
        Primer_Nombre AS PrimerNombre,
        Segundo_Nombre AS SegundoNombre,
        Primer_Apellido AS PrimerApellido,
        Segundo_Apellido AS SegundoApellido,
        Departamento AS Departamento
    FROM Empleados;
END //

CREATE PROCEDURE sp_LeerEmpleadosPorDepartamento(
    IN departamento VARCHAR(100)
)
BEGIN
    SELECT 
        Codigo_Empleado AS CodigoEmpleado,
        Contrasena AS Contraseña,
        Primer_Nombre AS PrimerNombre,
        Segundo_Nombre AS SegundoNombre,
        Primer_Apellido AS PrimerApellido,
        Segundo_Apellido AS SegundoApellido,
        Departamento AS Departamento
    FROM Empleados
    WHERE Departamento = departamento;
END //

CREATE PROCEDURE sp_ActualizarEmpleado(
    IN codigoEmpleado VARCHAR(50),
    IN contrasena VARCHAR(255),
    IN primerNombre VARCHAR(100),
    IN segundoNombre VARCHAR(100),
    IN primerApellido VARCHAR(100),
    IN segundoApellido VARCHAR(100),
    IN departamento VARCHAR(100)
)
BEGIN
    UPDATE Empleados 
    SET Contrasena = contrasena, 
        Primer_Nombre = primerNombre, 
        Segundo_Nombre = segundoNombre, 
        Primer_Apellido = primerApellido, 
        Segundo_Apellido = segundoApellido, 
        Departamento = departamento
    WHERE Codigo_Empleado = codigoEmpleado;
END //

CREATE PROCEDURE sp_EliminarEmpleado(
    IN id INT
)
BEGIN
    DELETE FROM Empleados WHERE Codigo_Empleado = id;
END //

DELIMITER ;

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
DELIMITER //

CREATE PROCEDURE sp_InsertarEstudiante(
    IN carnet VARCHAR(50),
    IN contrasena VARCHAR(255),
    IN primerNombre VARCHAR(100),
    IN segundoNombre VARCHAR(100),
    IN primerApellido VARCHAR(100),
    IN segundoApellido VARCHAR(100),
    IN carrera VARCHAR(100),
    IN añoIngreso INT
)
BEGIN
    INSERT INTO Estudiantes (Carnet, Contrasena, Primer_Nombre, Segundo_Nombre, Primer_Apellido, Segundo_Apellido, Carrera, Anio_Ingreso) 
    VALUES (carnet, contrasena, primerNombre, segundoNombre, primerApellido, segundoApellido, carrera, añoIngreso);
END //

CREATE PROCEDURE sp_LeerEstudiantes()
BEGIN
    SELECT 
        Carnet AS Carnet,
        Contrasena AS Contraseña,
        Primer_Nombre AS PrimerNombre,
        Segundo_Nombre AS SegundoNombre,
        Primer_Apellido AS PrimerApellido,
        Segundo_Apellido AS SegundoApellido,
        Carrera AS Carrera,
        Anio_Ingreso AS AñoIngreso,
        Cantidad_Faltas AS CantidadFaltas
    FROM Estudiantes;
END //

CREATE PROCEDURE sp_LeerEstudiantesPorCarnet(
    IN carnet VARCHAR(50)
)
BEGIN
    SELECT 
        Carnet AS Carnet,
        Contrasena AS Contraseña,
        Primer_Nombre AS PrimerNombre,
        Segundo_Nombre AS SegundoNombre,
        Primer_Apellido AS PrimerApellido,
        Segundo_Apellido AS SegundoApellido,
        Carrera AS Carrera,
        Anio_Ingreso AS AñoIngreso,
        Cantidad_Faltas AS CantidadFaltas
    FROM Estudiantes
    WHERE Carnet = carnet;
END //

CREATE PROCEDURE sp_ActualizarEstudiante(
    IN carnet VARCHAR(50),
    IN contrasena VARCHAR(255),
    IN primerNombre VARCHAR(100),
    IN segundoNombre VARCHAR(100),
    IN primerApellido VARCHAR(100),
    IN segundoApellido VARCHAR(100),
    IN carrera VARCHAR(100),
    IN cantidadFaltas INT
)
BEGIN
    UPDATE Estudiantes 
    SET Contrasena = contrasena, 
        Primer_Nombre = primerNombre, 
        Segundo_Nombre = segundoNombre, 
        Primer_Apellido = primerApellido, 
        Segundo_Apellido = segundoApellido, 
        Carrera = carrera,
        Cantidad_Faltas = cantidadFaltas
    WHERE Carnet = carnet;
END //

CREATE PROCEDURE sp_EliminarEstudiante(
    IN id INT
)
BEGIN
    DELETE FROM Estudiantes WHERE Carnet = id;
END //

DELIMITER ;

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

-- CRUD de GRUPOS
DELIMITER //

CREATE PROCEDURE sp_InsertarGrupo(
    IN codidoGrupo VARCHAR(50),
    IN idAsignatura VARCHAR(100),
    IN idDocente VARCHAR(50),
    IN seccion VARCHAR(50)
)
BEGIN
    INSERT INTO Grupos (Codigo_Grupo, Id_Asignatura, Carnet_Docente, Seccion) 
    VALUES (codidoGrupo, idAsignatura, idDocente, seccion);
END //

CREATE PROCEDURE sp_LeerGrupos()
BEGIN
    SELECT 
        Codigo_Grupo AS CodidoGrupo,
        Id_Asignatura AS IdAsignatura,
        Carnet_Docente AS IdDocente,
        Seccion AS Seccion
    FROM Grupos;
END //

CREATE PROCEDURE sp_LeerGrupoPorCodigo(
    IN codidoGrupo VARCHAR(50)
)
BEGIN
    SELECT 
        Codigo_Grupo AS CodidoGrupo,
        Id_Asignatura AS IdAsignatura,
        Carnet_Docente AS IdDocente,
        Seccion AS Seccion
    FROM Grupos
    WHERE Codigo_Grupo = codidoGrupo;
END //

CREATE PROCEDURE sp_ActualizarGrupo(
    IN codidoGrupo VARCHAR(50),
    IN idAsignatura VARCHAR(100),
    IN idDocente VARCHAR(50),
    IN seccion VARCHAR(50)
)
BEGIN
    UPDATE Grupos 
    SET Id_Asignatura = idAsignatura, 
        Carnet_Docente = idDocente, 
        Seccion = seccion 
    WHERE Codigo_Grupo = codidoGrupo;
END //

CREATE PROCEDURE sp_EliminarGrupo(
    IN id INT
)
BEGIN
    DELETE FROM Grupos WHERE Codigo_Grupo = id;
END //

DELIMITER ;
CREATE TABLE IF NOT EXISTS `Matriculas` (
	`Id_Matricula` INT AUTO_INCREMENT PRIMARY KEY,
	`Carnet_Estudiante` VARCHAR(50) NOT NULL,
	`Codigo_Grupo` VARCHAR(50) NOT NULL,
	`Fecha_Registro` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY(`Carnet_Estudiante`) REFERENCES `Estudiantes`(`Carnet`),
	FOREIGN KEY(`Codigo_Grupo`) REFERENCES `Grupos`(`Codigo_Grupo`)
);

-- CRUD de MATRICULAS
DELIMITER //

CREATE PROCEDURE sp_InsertarMatricula(
    IN idEstudiante VARCHAR(50),
    IN codidoGrupo VARCHAR(50)
)
BEGIN
    INSERT INTO Matriculas (Carnet_Estudiante, Codigo_Grupo) 
    VALUES (idEstudiante, codidoGrupo);
END //

CREATE PROCEDURE sp_LeerMatriculas()
BEGIN
    SELECT 
        Id_Matricula AS IdMatricula,
        Carnet_Estudiante AS IdEstudiante,
        Codigo_Grupo AS CodidoGrupo,
        Fecha_Registro AS FechaRegistro
    FROM Matriculas;
END //

CREATE PROCEDURE sp_LeerMatriculasPorEstudiante(
    IN idEstudiante VARCHAR(50)
)
BEGIN
    SELECT 
        Id_Matricula AS IdMatricula,
        Carnet_Estudiante AS IdEstudiante,
        Codigo_Grupo AS CodidoGrupo,
        Fecha_Registro AS FechaRegistro
    FROM Matriculas
    WHERE Carnet_Estudiante = idEstudiante;
END //

CREATE PROCEDURE sp_ActualizarMatricula(
    IN idMatricula INT,
    IN idEstudiante VARCHAR(50),
    IN codidoGrupo VARCHAR(50)
)
BEGIN
    UPDATE Matriculas 
    SET Carnet_Estudiante = idEstudiante, 
        Codigo_Grupo = codidoGrupo 
    WHERE Id_Matricula = idMatricula;
END //

CREATE PROCEDURE sp_EliminarMatricula(
    IN id INT
)
BEGIN
    DELETE FROM Matriculas WHERE Id_Matricula = id;
END //

DELIMITER ;

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
DELIMITER //

CREATE PROCEDURE sp_InsertarSesion(
    IN codigoGrupo VARCHAR(50),
    IN temaImpartido VARCHAR(255),
    IN fechaHoraInicio DATETIME,
    IN fechaHoraFin DATETIME
)
BEGIN
    INSERT INTO Sesiones_Clase (Codigo_Grupo, Fecha, Tema_Impartido, Hora_Inicio, Hora_Fin) 
    VALUES (codigoGrupo, CAST(fechaHoraInicio AS DATE), temaImpartido, CAST(fechaHoraInicio AS TIME), CAST(fechaHoraFin AS TIME));
END //

CREATE PROCEDURE sp_LeerSesiones()
BEGIN
    SELECT 
        S.Id_Sesion AS IdSesion,
        S.Codigo_Grupo AS CodigoGrupo,
        S.Tema_Impartido AS TemaImpartido,
        CAST(CONCAT(S.Fecha, ' ', S.Hora_Inicio) AS DATETIME) AS FechaHoraInicio,
        CAST(CONCAT(S.Fecha, ' ', S.Hora_Fin) AS DATETIME) AS FechaHoraFin,
        G.Seccion AS Seccion,
        D.Departamento AS Departamento
    FROM Sesiones_Clase S
    INNER JOIN Grupos G ON S.Codigo_Grupo = G.Codigo_Grupo
    LEFT JOIN Docentes D ON G.Carnet_Docente = D.Carnet;
END //

CREATE PROCEDURE sp_LeerSesionPorGrupo(
    IN idGrupo VARCHAR(50)
)
BEGIN
    SELECT 
        S.Id_Sesion AS IdSesion,
        S.Codigo_Grupo AS CodigoGrupo,
        S.Tema_Impartido AS TemaImpartido,
        CAST(CONCAT(S.Fecha, ' ', S.Hora_Inicio) AS DATETIME) AS FechaHoraInicio,
        CAST(CONCAT(S.Fecha, ' ', S.Hora_Fin) AS DATETIME) AS FechaHoraFin,
        G.Seccion AS Seccion,
        D.Departamento AS Departamento
    FROM Sesiones_Clase S
    INNER JOIN Grupos G ON S.Codigo_Grupo = G.Codigo_Grupo
    LEFT JOIN Docentes D ON G.Carnet_Docente = D.Carnet
    WHERE S.Codigo_Grupo = idGrupo;
END //

CREATE PROCEDURE sp_ActualizarSesion(
    IN idSesion INT,
    IN codigoGrupo VARCHAR(50),
    IN temaImpartido VARCHAR(255),
    IN fechaHoraInicio DATETIME,
    IN fechaHoraFin DATETIME
)
BEGIN
    UPDATE Sesiones_Clase 
    SET Codigo_Grupo = codigoGrupo,
        Fecha = CAST(fechaHoraInicio AS DATE),
        Tema_Impartido = temaImpartido, 
        Hora_Inicio = CAST(fechaHoraInicio AS TIME), 
        Hora_Fin = CAST(fechaHoraFin AS TIME) 
    WHERE Id_Sesion = idSesion;
END //

CREATE PROCEDURE sp_EliminarSesion(
    IN id INT
)
BEGIN
    DELETE FROM Sesiones_Clase WHERE Id_Sesion = id;
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


USE AsistenciaUniversitaria;

-- 1. Desactivar restricciones para vaciar las tablas de prueba limpiamente
SET FOREIGN_KEY_CHECKS = 0;

TRUNCATE TABLE `Asistencia`;
TRUNCATE TABLE `Auditoria_Log`;
TRUNCATE TABLE `Estudiantes_Sancionados`;
TRUNCATE TABLE `Matriculas`;
TRUNCATE TABLE `Sesiones_Clase`;
TRUNCATE TABLE `Grupos`;
TRUNCATE TABLE `Docentes`;
TRUNCATE TABLE `Empleados`;
TRUNCATE TABLE `Estudiantes`;

SET FOREIGN_KEY_CHECKS = 1;

-- =========================================================
-- 2. INSERCIÓN EN TABLA: Empleados (15 registros)
-- =========================================================
INSERT INTO `Empleados` (`Codigo_Empleado`, `Contrasena`, `Primer_Nombre`, `Segundo_Nombre`, `Primer_Apellido`, `Segundo_Apellido`, `Departamento`) VALUES
('E001', 'PassEncrypted01$', 'Carlos', 'Alberto', 'García', 'Mendoza', 'Registro Académico'),
('E002', 'PassEncrypted02$', 'María', 'Fernanda', 'López', 'Rios', 'Tecnología e Informática'),
('E003', 'PassEncrypted03$', 'Jorge', 'Luis', 'Martínez', 'Sánchez', 'Atención al Estudiante'),
('E004', 'PassEncrypted04$', 'Ana', 'Lucía', 'Gómez', 'Espinosa', 'Recursos Humanos'),
('E005', 'PassEncrypted05$', 'Roberto', 'Antonio', 'Pérez', 'Torres', 'Contabilidad'),
('E006', 'PassEncrypted06$', 'Sonia', 'Beatriz', 'Hernández', 'Castillo', 'Decanatura'),
('E007', 'PassEncrypted07$', 'Gabriel', 'José', 'Ruiz', 'Gaitán', 'Biblioteca'),
('E008', 'PassEncrypted08$', 'Elena', 'Patricia', 'Morales', 'Suárez', 'Bienestar Estudiantil'),
('E009', 'PassEncrypted09$', 'Fernando', 'Javier', 'Silva', 'Valenzuela', 'Tecnología e Informática'),
('E010', 'PassEncrypted10$', 'Carmen', 'Rosa', 'Vargas', 'Reyes', 'Registro Académico'),
('E011', 'PassEncrypted11$', 'Ricardo', 'Andrés', 'Castro', 'Mejía', 'Mantenimiento e Infraestructura'),
('E012', 'PassEncrypted12$', 'Patricia', 'Isabel', 'Navarro', 'Salazar', 'Tesorería'),
('E013', 'PassEncrypted13$', 'Héctor', 'Manuel', 'Ríos', 'Guerrero', 'Sistemas Operativos'),
('E014', 'PassEncrypted14$', 'Claudia', 'Lisseth', 'Pineda', 'Blandón', 'Secretaría General'),
('E015', 'PassEncrypted15$', 'Mauricio', 'Esteban', 'Paredes', 'Obando', 'Coordinación Académica');

-- =========================================================
-- 3. INSERCIÓN EN TABLA: Docentes (15 registros)
-- =========================================================
INSERT INTO `Docentes` (`Carnet`, `Contrasena`, `Primer_Nombre`, `Segundo_Nombre`, `Primer_Apellido`, `Segundo_Apellido`, `Codigo_Empleado`, `Departamento`, `Cantidad_Faltas`) VALUES
('D-240101', 'VidalSecure2026', 'Vidal', 'José', 'Herrera', 'Gutiérrez', 'D001', 'Ingeniería en Sistemas', 0),
('D-240102', 'DavidSecure2026', 'David', 'de Jesús', 'Lovo', 'Moreira', 'D002', 'Ingeniería en Sistemas', 0),
('D-240103', 'EduardoSecure26', 'Eduardo', 'Alfonso', 'Hernández', 'Alvarado', 'D003', 'Finanzas', 0),
('D-240104', 'DocPass_2026_04', 'Maritza', 'Elena', 'Guzmán', 'Siles', 'D004', 'Ingeniería en Sistemas', 0),
('D-240105', 'DocPass_2026_05', 'Francisco', 'Javier', 'Solis', 'Barreda', 'D005', 'Ciencias Básicas', 0),
('D-240106', 'DocPass_2026_06', 'Brenda', 'Margarita', 'Urbina', 'Cajina', 'D006', 'Administración de Empresas', 0),
('D-240107', 'DocPass_2026_07', 'Gonzalo', 'Adolfo', 'Noguera', 'Zapata', 'D007', 'Ingeniería Industrial', 0),
('D-240108', 'DocPass_2026_08', 'Lilliam', 'Socorro', 'Moncada', 'Rivas', 'D008', 'Ciencias Básicas', 0),
('D-240109', 'DocPass_2026_09', 'Ernesto', 'Camilo', 'Baltodano', 'Fonseca', 'D009', 'Derecho', 0),
('D-240110', 'DocPass_2026_10', 'Xochilt', 'Tania', 'Alemán', 'Mayorga', 'D010', 'Marketing', 0),
('D-240111', 'DocPass_2026_11', 'Julio', 'César', 'Salinas', 'Mendoza', 'D011', 'Ingeniería en Sistemas', 0),
('D-240112', 'DocPass_2026_12', 'Rosa', 'Amelia', 'Tercero', 'Espinoza', 'D012', 'Finanzas', 0),
('D-240113', 'DocPass_2026_13', 'Mario', 'René', 'Aguilar', 'Sánchez', 'D013', 'Ingeniería Industrial', 0),
('D-240114', 'DocPass_2026_14', 'Kenia', 'Vanessa', 'Pastora', 'Tinoco', 'D014', 'Arquitectura', 0),
('D-240115', 'DocPass_2026_15', 'Álvaro', 'José', 'Cordero', 'Centeno', 'D015', 'Derecho', 0);

-- =========================================================
-- 4. INSERCIÓN EN TABLA: Estudiantes (20 registros)
-- =========================================================
INSERT INTO `Estudiantes` (`Carnet`, `Contrasena`, `Primer_Nombre`, `Segundo_Nombre`, `Primer_Apellido`, `Segundo_Apellido`, `Carrera`, `Anio_Ingreso`, `Cantidad_Faltas`) VALUES
('240871', 'PassEst_240871', 'Gustavo', 'Adolfo', 'Dávila', 'Maradiaga', 'Ingeniería en Sistemas', 2024, 0),
('240908', 'PassEst_240908', 'Jizmeth', 'Raúl', 'Lainez', 'Chávez', 'Ingeniería en Sistemas', 2024, 0),
('240909', 'PassEst_240909', 'Víctor', 'Miguel', 'Romero', 'Espinoza', 'Ingeniería en Sistemas', 2024, 0),
('240101', 'PassEst_240101', 'Alejandro', 'José', 'Martínez', 'Lumbí', 'Ingeniería en Sistemas', 2024, 0),
('240102', 'PassEst_240102', 'Sofia', 'Isabel', 'Ramírez', 'Reyes', 'Ingeniería en Sistemas', 2024, 0),
('240103', 'PassEst_240103', 'Diego', 'Armando', 'Montenegro', 'Sequeira', 'Ingeniería Industrial', 2024, 0),
('240104', 'PassEst_240104', 'Camila', 'Andrea', 'Torres', 'Valdivia', 'Administración de Empresas', 2024, 0),
('240105', 'PassEst_240105', 'Mateo', 'Sebastián', 'Chavarría', 'Orozco', 'Ingeniería en Sistemas', 2024, 0),
('240106', 'PassEst_240106', 'Valeria', 'Nicole', 'Estrada', 'Giron', 'Marketing', 2024, 0),
('240107', 'PassEst_240107', 'Daniel', 'Esteban', 'Suárez', 'Borge', 'Finanzas', 2024, 0),
('240108', 'PassEst_240108', 'Lucía', 'Fernanda', 'Mendoza', 'Aragón', 'Derecho', 2024, 0),
('240109', 'PassEst_240109', 'Gabriel', 'Ignacio', 'Rivas', 'Centeno', 'Ingeniería Industrial', 2024, 0),
('240110', 'PassEst_240110', 'Mariana', 'Beatriz', 'Cabrera', 'Molina', 'Ingeniería en Sistemas', 2024, 0),
('240111', 'PassEst_240111', 'Samuel', 'David', 'Ramos', 'Noguera', 'Arquitectura', 2024, 0),
('240112', 'PassEst_240112', 'Natalia', 'Carolina', 'Ortega', 'Zelaya', 'Finanzas', 2024, 0),
('240113', 'PassEst_240113', 'Adrian', 'Felipe', 'Corea', 'López', 'Ingeniería en Sistemas', 2024, 0),
('240114', 'PassEst_240114', 'Isabella', 'Monserrat', 'Salgado', 'Godoy', 'Marketing', 2024, 0),
('240115', 'PassEst_240115', 'Lucas', 'Enrique', 'Pineda', 'Urbina', 'Derecho', 2024, 0),
('240116', 'PassEst_240116', 'Genesis', 'Abigail', 'Madrigal', 'Jarquin', 'Administración de Empresas', 2024, 0),
('240117', 'PassEst_240117', 'Joaquín', 'Emanuel', 'Solórzano', 'Picado', 'Ingeniería Industrial', 2024, 0);

-- =========================================================
-- 5. INSERCIÓN EN TABLA: Grupos (15 registros)
-- =========================================================
INSERT INTO `Grupos` (`Codigo_Grupo`, `Id_Asignatura`, `Carnet_Docente`, `Seccion`) VALUES
('MI08C', 'Programación Orientada a Objetos', 'D-240101', 'L12'),
('MI08D', 'Estructuras de Datos', 'D-240102', 'L13'),
('FIN01A', 'Contabilidad Financiera', 'D-240103', 'A01'),
('MI09A', 'Bases de Datos Avanzadas', 'D-240104', 'L14'),
('CB01B', 'Cálculo Multivariable', 'D-240105', 'B02'),
('ADM02C', 'Gestión del Talento Humano', 'D-240106', 'C03'),
('IND03A', 'Investigación de Operaciones', 'D-240107', 'I01'),
('CB02A', 'Física General', 'D-240108', 'B01'),
('DER01A', 'Derecho Constitucional', 'D-240109', 'D01'),
('MKT01C', 'Investigación de Mercados', 'D-240110', 'M02'),
('MI10A', 'Arquitectura de Software', 'D-240111', 'L15'),
('FIN02B', 'Análisis de Costos', 'D-240112', 'A02'),
('IND04B', 'Control de Calidad', 'D-240113', 'I02'),
('ARQ01A', 'Diseño Arquitectónico I', 'D-240114', 'Q01'),
('DER02B', 'Derecho Mercantil', 'D-240115', 'D02');

-- =========================================================
-- 6. INSERCIÓN EN TABLA: Matriculas (20 registros)
-- =========================================================
INSERT INTO `Matriculas` (`Carnet_Estudiante`, `Codigo_Grupo`, `Fecha_Registro`) VALUES
('240871', 'MI08C', '2026-07-01 08:00:00'),
('240908', 'MI08C', '2026-07-01 08:05:00'),
('240909', 'MI08C', '2026-07-01 08:10:00'),
('240101', 'MI08D', '2026-07-01 08:15:00'),
('240102', 'MI09A', '2026-07-01 08:20:00'),
('240103', 'IND03A', '2026-07-01 08:25:00'),
('240104', 'ADM02C', '2026-07-01 08:30:00'),
('240105', 'MI08C', '2026-07-01 08:35:00'),
('240106', 'MKT01C', '2026-07-01 08:40:00'),
('240107', 'FIN01A', '2026-07-01 08:45:00'),
('240108', 'DER01A', '2026-07-01 08:50:00'),
('240109', 'IND04B', '2026-07-01 08:55:00'),
('240110', 'MI10A', '2026-07-01 09:00:00'),
('240111', 'ARQ01A', '2026-07-01 09:05:00'),
('240112', 'FIN02B', '2026-07-01 09:10:00'),
('240113', 'MI08D', '2026-07-01 09:15:00'),
('240114', 'MKT01C', '2026-07-01 09:20:00'),
('240115', 'DER02B', '2026-07-01 09:25:00'),
('240116', 'ADM02C', '2026-07-01 09:30:00'),
('240117', 'IND03A', '2026-07-01 09:35:00');

-- =========================================================
-- 7. INSERCIÓN EN TABLA: Sesiones_Clase (15 registros)
-- =========================================================
INSERT INTO `Sesiones_Clase` (`Codigo_Grupo`, `Fecha`, `Tema_Impartido`, `Hora_Inicio`, `Hora_Fin`) VALUES
('MI08C', '2026-07-10', 'Introducción a Clases y Objetos', '08:00:00', '10:00:00'),
('MI08C', '2026-07-12', 'Encapsulamiento y Métodos Acceso', '08:00:00', '10:00:00'),
('MI08D', '2026-07-10', 'Listas Enlazadas Simples y Dobles', '10:00:00', '12:00:00'),
('FIN01A', '2026-07-11', 'Principios de Contabilidad General', '07:00:00', '09:00:00'),
('MI09A', '2026-07-11', 'Optimización de Consultas SQL', '13:00:00', '15:00:00'),
('CB01B', '2026-07-12', 'Derivadas Parciales y Aplicaciones', '09:00:00', '11:00:00'),
('ADM02C', '2026-07-13', 'Gestión del Desempeño Laboral', '10:00:00', '12:00:00'),
('IND03A', '2026-07-13', 'Método Simplex para Optimización', '14:00:00', '16:00:00'),
('CB02A', '2026-07-14', 'Leyes de Newton y Dinámica', '08:00:00', '10:00:00'),
('DER01A', '2026-07-14', 'Garantías Constitucionales', '11:00:00', '13:00:00'),
('MKT01C', '2026-07-15', 'Segmentación de Mercado Objetivo', '15:00:00', '17:00:00'),
('MI10A', '2026-07-15', 'Patrones de Diseño Microservicios', '08:00:00', '10:00:00'),
('FIN02B', '2026-07-16', 'Punto de Equilibrio en la Empresa', '10:00:00', '12:00:00'),
('IND04B', '2026-07-16', 'Gráficos de Control Estadístico', '13:00:00', '15:00:00'),
('ARQ01A', '2026-07-17', 'Zonificación de Espacios Urbanos', '09:00:00', '12:00:00');

-- =========================================================
-- 8. INSERCIÓN EN TABLA: Asistencia (18 registros)
-- =========================================================
INSERT INTO `Asistencia` (`Id_Sesion`, `Carnet_Usuario`, `Estado`, `Hora_Registro`) VALUES
(1, '240871', 'Presente', '08:02:15'),
(1, '240908', 'Presente', '08:04:30'),
(1, '240909', 'Tarde', '08:16:05'),
(1, '240105', 'Presente', '07:58:40'),
(2, '240871', 'Presente', '08:01:10'),
(2, '240908', 'Justificado', '08:30:00'),
(2, '240909', 'Presente', '08:00:05'),
(3, '240101', 'Presente', '10:01:12'),
(3, '240113', 'Ausente', NULL),
(4, '240107', 'Presente', '07:05:00'),
(5, '240102', 'Presente', '13:00:22'),
(6, '240103', 'Tarde', '09:12:45'),
(7, '240104', 'Presente', '10:00:10'),
(7, '240116', 'Presente', '10:02:18'),
(8, '240103', 'Presente', '14:01:00'),
(8, '240117', 'Ausente', NULL),
(11, '240106', 'Presente', '15:03:11'),
(11, '240114', 'Presente', '15:01:45');

-- =========================================================
-- 9. INSERCIÓN EN TABLA: Auditoria_Log (15 registros)
-- =========================================================
INSERT INTO `Auditoria_Log` (`Accion`, `Tabla_Afectada`, `Dato_Identificador`, `Fecha_Accion`) VALUES
('INSERT', 'Estudiantes', 'Carnet: 240871', '2026-07-01 08:00:00'),
('INSERT', 'Estudiantes', 'Carnet: 240908', '2026-07-01 08:05:00'),
('INSERT', 'Estudiantes', 'Carnet: 240909', '2026-07-01 08:10:00'),
('INSERT', 'Docentes', 'Carnet: D-240101', '2026-07-01 08:15:00'),
('INSERT', 'Docentes', 'Carnet: D-240102', '2026-07-01 08:20:00'),
('INSERT', 'Grupos', 'Codigo_Grupo: MI08C', '2026-07-01 08:25:00'),
('INSERT', 'Grupos', 'Codigo_Grupo: MI08D', '2026-07-01 08:30:00'),
('INSERT', 'Matriculas', 'Carnet: 240871 | Grupo: MI08C', '2026-07-01 08:35:00'),
('INSERT', 'Matriculas', 'Carnet: 240908 | Grupo: MI08C', '2026-07-01 08:40:00'),
('INSERT', 'Sesiones_Clase', 'Id_Sesion: 1 | Grupo: MI08C', '2026-07-10 08:00:00'),
('UPDATE', 'Asistencia', 'Id_Asistencia: 3 | Estado: Tarde', '2026-07-10 08:16:05'),
('INSERT', 'Sesiones_Clase', 'Id_Sesion: 2 | Grupo: MI08C', '2026-07-12 08:00:00'),
('UPDATE', 'Asistencia', 'Id_Asistencia: 6 | Estado: Justificado', '2026-07-12 08:30:00'),
('INSERT', 'Empleados', 'Codigo_Empleado: E001', '2026-07-13 09:00:00'),
('INSERT', 'Empleados', 'Codigo_Empleado: E002', '2026-07-13 09:15:00');