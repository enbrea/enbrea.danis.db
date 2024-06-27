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

using System;
using System.Threading.Tasks;

namespace Enbrea.Danis.Db.SmokeTest
{
    public class AppService
    {
        private readonly AppConfig _appConfig;

        public AppService(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("DaNiS Export Test");
            Console.WriteLine("-----------------\n");

            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\t1 - Export Groups (Klassen)");
            Console.WriteLine("\t2 - Export Students (Schüler)");
            Console.WriteLine("\t3 - Export Teachers (Lehrer)");
            Console.WriteLine("\t4 - Export Courses (Kurse)");
            Console.Write("Your selection? ");

            switch (Console.ReadLine())
            {
                case "1":
                    await ExportGroups();
                    break;
                case "2":
                    await ExportStudents();
                    break;
                case "3":
                    await ExportTeachers();
                    break;
                case "4":
                    await ExportCourses();
                    break;
            }
        }

        private DanisDbReader CreateDbReader()
        {
            return new DanisDbReader(_appConfig.DbConnection);
        }

        private async Task ExportCourses()
        {
            var dbReader = CreateDbReader();

            Console.WriteLine();
            Console.WriteLine("Courses:");
            Console.WriteLine("--------");

            await foreach (var group in dbReader.CoursesAsync(2023))
            {
                Console.WriteLine(@"{0}, {1}",
                    group.Id,
                    group.Name);
            }
        }

        private async Task ExportGroups()
        {
            var dbReader = CreateDbReader();

            Console.WriteLine();
            Console.WriteLine("Groups:");
            Console.WriteLine("-------");

            await foreach (var group in dbReader.GroupsAsync(2023))
            {
                Console.WriteLine(@"{0}, {1}", 
                    group.Id, 
                    group.Name);
            }
        }

        private async Task ExportStudents()
        {
            var dbReader = CreateDbReader();

            Console.WriteLine();
            Console.WriteLine("Students:");
            Console.WriteLine("---------");

            await foreach (var student in dbReader.StudentsAsync(2023))
            {
                Console.WriteLine(@"{0}, {1}, {2}, {3}, {4}, {5}",
                    student.Id,
                    student.NickName,
                    student.LastName, 
                    student.FirstNames,
                    student.Birthdate?.ToString("dd.MM.yyyy"),
                    student.Gender,
                    student.Locality, 
                    student.Street);
            }
        }

        private async Task ExportTeachers()
        {
            var dbReader = CreateDbReader();

            Console.WriteLine();
            Console.WriteLine("Teachers:");
            Console.WriteLine("---------");

            await foreach (var teacher in dbReader.TeachersAsync())
            {
                Console.WriteLine(@"{0}, {1}, {2}, {3}, {4}, {5}",
                    teacher.Id,
                    teacher.NickName,
                    teacher.LastName, 
                    teacher.FirstNames, 
                    teacher.Birthdate?.ToString("dd.MM.yyyy"),
                    teacher.Gender,
                    teacher.Locality);
            }
        }
    }
}
