#region Enbrea - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    Enbrea
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU Affero General Public License, version 3,
 *    as published by the Free Software Foundation.
 *
 *    This program is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *    GNU Affero General Public License for more details.
 *
 *    You should have received a copy of the GNU Affero General Public License
 *    along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 */
#endregion

using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Enbrea.Danis.Db
{
    public class DanisDbReader
    {
        private readonly string _dbConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DanisDbReader<T>"/> class.
        /// </summary>
        public DanisDbReader(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns back all countries (Staaten)
        /// </summary>
        /// <returns>An async enumerator of countries</returns>
        public async IAsyncEnumerable<Country> CountriesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Country.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT 
                      L1.Id,
                      L1.Code, 
                      L1.Bezeichnung, 
                      L2.Kennzahl, 
                      L2.StatRel
                    FROM
                      landiso L1
                    LEFT JOIN
                      land L2 ON L1.LandId = L2.Id
                    WHERE
                      Deleted = 0
                    """;
            }
        }

        /// <summary>
        /// Returns back all courses (Kurse)
        /// </summary>
        /// <param name="year">Year when the school year starts</param>
        /// <returns>An async enumerator of courses</returns>
        public async IAsyncEnumerable<Course> CoursesAsync(int year)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Course.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT 
                      K.Id,
                      K.Bezeichnung,
                      K.Spezial,
                      K.LehrerId,
                      K.FachId,
                      K.Von,
                      K.Bis,
                      SG.SchulformId,
                      SG.SchuljahrId
                    FROM 
                      kurs K
                    JOIN 
                      schulgliederung SG ON SG.Id = K.SchulgliederungId
                    JOIN 
                      schuljahr SJ ON SG.SchuljahrId = SJ.Id
                    WHERE 
                      SJ.Jahr = @year
                    """;

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "year";
                dbParameter.Value = year;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all groups (Gruppen)
        /// </summary>
        /// <param name="year">Year when the school year starts</param>
        /// <returns>An async enumerator of groups</returns>
        public async IAsyncEnumerable<Group> GroupsAsync(int year)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Group.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT 
                      G.Id,
                      G.Bezeichnung,
                      G.LehrerId1,
                      G.LehrerId2,
                      G.Jahrgangsstufe,
                      G.Raum,
                      G.StandortID,
                      SG.SchulformId,
                      SG.SchuljahrId
                    FROM 
                      gruppe G
                    JOIN 
                      schulgliederung SG ON SG.Id = G.SchulgliederungId
                    JOIN 
                      schuljahr SJ ON SG.SchuljahrId = SJ.Id
                    WHERE 
                      SJ.Jahr = @year
                    """;

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "year";
                dbParameter.Value = year;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all languages (Sprachen)
        /// </summary>
        /// <returns>An async enumerator of languages</returns>
        public async IAsyncEnumerable<Language> LanguagesAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Language.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT 
                      Id, 
                      Bezeichnung, 
                      Kennzahl, 
                      StatRel
                    FROM 
                      sprache 
                    WHERE
                      Deleted = 0
                    """;
            }
        }

        /// <summary>
        /// Returns back all locations (Standorte)
        /// </summary>
        /// <returns>An async enumerator of locations</returns>
        public async IAsyncEnumerable<Location> LocationsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Location.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT 
                      Id, 
                      Nr, 
                      Name, 
                      Adresse, 
                      PLZ, 
                      Ort, 
                      Telefon,
                      Fax,
                      EMail
                    FROM
                      standort
                    """;
            }
        }

        /// <summary>
        /// Returns back all religions (Konfessionen)
        /// </summary>
        /// <returns>An async enumerator of religions</returns>
        public async IAsyncEnumerable<Religion> ReligionsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Religion.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT
                      Id,
                      Bezeichnung, 
                      Kennzahl, 
                      StatRel
                    FROM 
                      konfession 
                    WHERE
                      Deleted = 0
                    """;
            }
        }

        /// <summary>
        /// Returns back all school forms (Schulformen)
        /// </summary>
        /// <returns>An async enumerator of school forms</returns>
        public async IAsyncEnumerable<SchoolForm> SchoolFormsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => SchoolForm.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT
                      Id, 
                      Kuerzel, 
                      Bezeichnung, 
                      Kennzahl, 
                      StatRel
                    FROM 
                      schulform
                    WHERE
                      Deleted = 0
                    """;
            }
        }

        /// <summary>
        /// Returns back all school years (Schuljahre)
        /// </summary>
        /// <returns>An async enumerator of school years</returns>
        public async IAsyncEnumerable<SchoolYear> SchoolYearsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => SchoolYear.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT
                      Id, 
                      Bezeichnung, 
                      Aktuell, 
                      Jahr
                    FROM 
                      schuljahr
                    """;
            }
        }

        /// <summary>
        /// Returns back all stundent course attendance (Kursbelegungen)
        /// </summary>
        /// <param name="year">Year when the school year starts</param>
        /// <returns>An async enumerator of student course attendances</returns>
        public async IAsyncEnumerable<StudentCourseAttendance> StudentCourseAttendancesAsync(int year)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => StudentCourseAttendance.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT 
                      B.Id,
                      B.KursBezeichnung,
                      B.KursId,
                      JD.SchuelerId,
                      JD.GruppeId,
                      K.LehrerId,
                      K.FachId
                    FROM 
                      belegung B
                    JOIN 
                      jahrgangsdaten JD ON JD.Id = B.JahrgangsdatenId
                    JOIN 
                      gruppe G ON JD.GruppeId = G.Id
                    JOIN 
                      schulgliederung SG ON SG.Id = G.SchulgliederungId
                    JOIN 
                      schuljahr SJ ON SG.SchuljahrId = SJ.Id
                    LEFT JOIN 
                      kurs K ON B.KursId = K.Id
                    WHERE 
                      SJ.Jahr = @year
                    """;

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "year";
                dbParameter.Value = year;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all student-group-relationships for a given school year (Schuljahr)
        /// </summary>
        /// <param name="year">Year when the school year starts</param>
        /// <returns>An async enumerator of student group attendances</returns>
        public async IAsyncEnumerable<StudentGroupAttendance> StudentGroupAttendancesAsync(int year)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => StudentGroupAttendance.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT 
                      JD.Id,
                      JD.SchuelerId,
                      JD.GruppeId, 
                      JD.Jahrgangsstufe
                    FROM 
                      jahrgangsdaten JD
                    JOIN 
                      gruppe G ON JD.GruppeId = G.Id
                    JOIN 
                      schulgliederung SG ON SG.Id = G.SchulgliederungId
                    JOIN 
                      schuljahr SJ ON SG.SchuljahrId = SJ.Id
                    WHERE 
                      SJ.Jahr = @year
                    """;

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "year";
                dbParameter.Value = year;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all students (Schüler)
        /// </summary>
        /// <param name="year">Year when the school year starts</param>
        /// <returns>An async enumerator of students</returns>
        public async IAsyncEnumerable<Student> StudentsAsync(int year)
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Student.FromDb(reader)))
            {
                yield return entity;
            }

            void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT 
                      P.Id,
                      P.Rufname, 
                      P.Nachname, 
                      P.Vornamen,
                      P.Namenszusatz,
                      P.Titel,
                      P.AkadTitel,
                      P.Geburtsdatum,
                      P.Geburtsort,
                      P.Geschlecht,
                      P.KonfessionId,
                      P.StaatsangehoerigkeitId,
                      P.Staatsangehoerigkeit2Id,
                      S.GeburtslandId,
                      S.Aufnahmedatum, 
                      S.VonSchule, 
                      S.HerkunftId,
                      S.Abgangsdatum,
                      S.NachSchule, 
                      S.NachSchulformId,
                      S.AbschlussId, 
                      S.ErstEinschulJahr, 
                      S.VerkehrsspracheId,
                      S.KrankenkasseId,
                      S.Fahrschueler,
                      S.Bemerkungen,
                      S.Wiederholer,
                      S.SchuelerNotfall,
                      A.Strasse,
                      O.PLZ,
                      O.Name as Ort,
                      O.Ortsteil,
                      O.Landkreis
                    FROM 
                      schueler S
                    JOIN 
                      person P ON P.Id = S.Id
                    JOIN 
                      jahrgangsdaten JD ON JD.SchuelerId = S.Id
                    JOIN 
                      gruppe G ON JD.GruppeId = G.Id
                    JOIN 
                      schulgliederung SG ON SG.Id = G.SchulgliederungId
                    JOIN 
                      schuljahr SJ ON SG.SchuljahrId = SJ.Id
                    LEFT JOIN 
                      adresse A ON A.PersonId = P.Id
                    LEFT JOIN 
                      ort O ON A.OrtId = O.Id 
                    WHERE 
                      SJ.Jahr = @year AND A.Hauptwohnsitz = 1
                    """;

                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = "year";
                dbParameter.Value = year;

                dbCommand.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Returns back all subjects (Fächer)
        /// </summary>
        /// <returns>An async enumerator of subjects</returns>
        public async IAsyncEnumerable<Subject> SubjectsAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Subject.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT
                      Id,
                      Bezeichnung, 
                      Kennzahl, 
                      StatRel
                    FROM 
                      fach 
                    WHERE
                      Deleted = 0
                    """;
            }
        }
        /// <summary>
        /// Returns back all teachers (Lehrer)
        /// </summary>
        /// <returns>An async enumerator of teachers</returns>
        public async IAsyncEnumerable<Teacher> TeachersAsync()
        {
            await foreach (var entity in EntitiesAsync(command => SetQuery(command), reader => Teacher.FromDb(reader)))
            {
                yield return entity;
            }

            static void SetQuery(DbCommand dbCommand)
            {
                dbCommand.CommandText =
                    """
                    SELECT 
                      P.Id,
                      P.Rufname, 
                      P.Nachname, 
                      P.Vornamen,
                      P.Namenszusatz,
                      P.Titel,
                      P.AkadTitel,
                      P.Geburtsdatum,
                      P.Geburtsort,
                      P.Geschlecht,
                      P.KonfessionId,
                      P.StaatsangehoerigkeitId,
                      P.Staatsangehoerigkeit2Id,
                      L.Kuerzel,
                      L.Personalnummer, 
                      L.Zugang,
                      L.Abgang,
                      L.LehramtId,
                      A.Strasse,
                      O.PLZ,
                      O.Name as Ort,
                      O.Ortsteil,
                      O.Landkreis
                    FROM 
                      lehrer L
                    JOIN 
                      person P ON P.Id = L.Id
                    LEFT JOIN 
                      adresse A ON A.PersonId = P.Id
                    LEFT JOIN 
                      ort O ON A.OrtId = O.Id 
                    WHERE 
                      A.Hauptwohnsitz = 1
                    """;
            }
        }

        /// <summary>
        /// Creates a MariaDB database connection
        /// </summary>
        /// <returns>The newly created database connection</returns>
        private DbConnection CreateConnection()
        {
            return new MySqlConnection(_dbConnectionString);
        }

        /// <summary>
        /// Opens the internal database connection, executes an SQL query and iterates over the result set.
        /// </summary>
        /// <typeparam name="TEntity">Enttiy type to be created</typeparam>
        /// <param name="setCommand">Action for initializing the sql command</param>
        /// <param name="createEntity">Action for creating a new TEntity instance</param>
        /// <returns>An async enumerator of TEntity instances</returns>
        private async IAsyncEnumerable<TEntity> EntitiesAsync<TEntity>(Action<DbCommand> setCommand, Func<DbDataReader, TEntity> createEntity)
        {
            using DbConnection dbConnection = CreateConnection();

            await dbConnection.OpenAsync();
            try
            {
                using var dbTransaction = dbConnection.BeginTransaction();
                using var dbCommand = dbConnection.CreateCommand();

                dbCommand.Transaction = dbTransaction;
                setCommand(dbCommand);

                using var reader = await dbCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    yield return createEntity(reader);
                }
            }
            finally
            {
                await dbConnection.CloseAsync();
            }
        }
    }
}
