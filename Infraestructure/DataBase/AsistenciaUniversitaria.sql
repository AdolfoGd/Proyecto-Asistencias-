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
