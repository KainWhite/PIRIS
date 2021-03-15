USE [master]
GO

------------------------------------------------------- kill all connections to database before drop -----------------------------------------------------------

DECLARE @SQL varchar(max)

SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
FROM MASTER..SysProcesses
WHERE DBId = DB_ID('banking_system') AND SPId <> @@SPId

--SELECT @SQL 
EXEC(@SQL)

------------------------------------------------------- -------------------------------------------- -----------------------------------------------------------

------------------------------------------------------------------ drop database banking_system ------------------------------------------------------------------

--if exists (select * from sys.databases where [name] = 'banking_system') drop database banking_system
--go

------------------------------------------------------------------ ---------------------------- ------------------------------------------------------------------

/****** Object:  Database [banking_system]    Script Date: 21-Feb-21 11:39:03 ******/
if not exists (select * from sys.databases where [name] = 'banking_system') begin

	CREATE DATABASE [banking_system]
	 CONTAINMENT = NONE
	 ON  PRIMARY 
	( NAME = N'banking_system', FILENAME = N'$(path)\banking_system.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
	 LOG ON 
	( NAME = N'banking_system_log', FILENAME = N'$(path)\banking_system_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )

	print 'Database ''banking_system'' was created successfully.';

end
go

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [banking_system].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [banking_system] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [banking_system] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [banking_system] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [banking_system] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [banking_system] SET ARITHABORT OFF 
GO

ALTER DATABASE [banking_system] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [banking_system] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [banking_system] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [banking_system] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [banking_system] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [banking_system] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [banking_system] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [banking_system] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [banking_system] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [banking_system] SET  DISABLE_BROKER 
GO

ALTER DATABASE [banking_system] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [banking_system] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [banking_system] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [banking_system] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [banking_system] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [banking_system] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [banking_system] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [banking_system] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [banking_system] SET  MULTI_USER 
GO

ALTER DATABASE [banking_system] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [banking_system] SET DB_CHAINING OFF 
GO

ALTER DATABASE [banking_system] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [banking_system] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [banking_system] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [banking_system] SET QUERY_STORE = OFF
GO

USE [banking_system]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [banking_system] SET  READ_WRITE 
GO

print 'Database variables of ''banking_system'' were configured.';
print '';
