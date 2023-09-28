using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace Sweeft_Tasks
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Subject { get; set; }

        public ICollection<Pupil> Pupils { get; set; } // Navigation property
    }

    public class Pupil
    {
        public int PupilId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }

        public ICollection<Teacher> Teachers { get; set; } // Navigation property
    }


    public class MyDB : DbContext
    {
        public MyDB(DbContextOptions<MyDB> options)
            : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            // TASK - N1
            

            Console.WriteLine("\nTASK N1 - is Palindrome: \n");
            bool IsPalindrome(string text)
            {
                text = text.ToLower();
                for (int i = 0, j = text.Length - 1; i < j; i++, j--)
                {
                    if (text[i] != text[j]) return false;
                }
                return true;
            }
            //Tests
            Console.WriteLine(IsPalindrome("aaasssaaa"));
            Console.WriteLine(IsPalindrome("dog"));
            Console.WriteLine(IsPalindrome("a"));
            Console.WriteLine(IsPalindrome(""));
            Console.WriteLine(IsPalindrome("ab"));


            Console.WriteLine("\nTASK N2 - MinSplit: \n");
            int MinSplit(int amount)
            {
                int[] coin = { 50, 20, 10, 5, 1 };
                int curr = 0;
                int ans = 0;
                while (amount > 0)
                {

                    if (amount - coin[curr] >= 0)
                    {
                        amount -= coin[curr];
                        ans++;
                    }
                    else curr++;
                }
                return ans;

            }
            //Tests
            Console.WriteLine(MinSplit(79));
            Console.WriteLine(MinSplit(100));
            Console.WriteLine(MinSplit(3));
            Console.WriteLine(MinSplit(21));

            Console.WriteLine("\nTASK N3 - NotContains: \n");

            int NotContains(int[] array)
            {
                Array.Sort(array);

                for (int i = 0; i < array.Length; i++)
                {
                    if (i + 1 != array[i]) return i + 1;
                }
                return array.Length;
            }
            int[] test = { 50, 20, 10, 5, 1, 2, 4, 5, 3, 7 };
            Console.WriteLine(NotContains(test));

            Console.WriteLine("\nTASK N4 - IsProperly: \n");

            bool IsProperly(string sequence)
            {
                int opened = 0;

                for (int i = 0; i < sequence.Length; i++)
                {
                    if (sequence[i] == '(') opened++;
                    else
                    {
                        if (opened == 0) return false;
                        else opened--;
                    }
                }
                if (opened > 0) return false;
                return true;
            }

            //Tests
            Console.WriteLine(IsProperly("(()())")); // True
            Console.WriteLine(IsProperly("((())())")); // True
            Console.WriteLine(IsProperly("(()")); // False
            Console.WriteLine(IsProperly(")()(")); // False
            Console.WriteLine(IsProperly("()()()")); // True
            Console.WriteLine(IsProperly("")); // True

            Console.WriteLine("\nTASK N5 - CountVariants: \n");


            int CountVariants(int stairCount)
            {
                if (stairCount <= 1) return 1;

                return CountVariants(stairCount - 1) + CountVariants(stairCount - 2);
            }

            Console.WriteLine(CountVariants(6));
            Console.WriteLine(CountVariants(3));
            Console.WriteLine(CountVariants(13));
            Console.WriteLine(CountVariants(9));

            Console.WriteLine("\nTASK N6 - SQL: \n");
            /*

            CREATE TABLE Teacher (
                TeacherID INT PRIMARY KEY AUTO_INCREMENT,
                FirstName VARCHAR(50),
                LastName VARCHAR(50),
                Gender CHAR(1),
                Subject VARCHAR(50)
            );

            CREATE TABLE Pupil (
                PupilID INT PRIMARY KEY AUTO_INCREMENT,
                FirstName VARCHAR(50),
                LastName VARCHAR(50),
                Gender CHAR(1),
                Class VARCHAR(50)
            );

            CREATE TABLE TeacherPupil (
                TeacherID INT,
                PupilID INT,
                PRIMARY KEY (TeacherID, PupilID),
                FOREIGN KEY (TeacherID) REFERENCES Teacher(TeacherID),
                FOREIGN KEY (PupilID) REFERENCES Pupil(PupilID)
            );





            */
            Console.WriteLine("\nTASK N7 - SQL: \n");

           /* Teacher[] GetAllTeachersByStudent(string studentName)
            {
                using (var context = new MyDB(new DbContextOptions<MyDB>()))
                {
                    var teachers = context.Teachers
                        .Where(teacher => teacher.Pupils.Any(pupil => pupil.FirstName == studentName))
                        .ToArray();

                    return teachers;
                }
            }
            var teachers = GetAllTeachersByStudent("giorgi");

            foreach (var teacher in teachers)
            {
                Console.WriteLine($"Teacher: {teacher.Name} {teacher.Surname}");
            }
           */







            Console.WriteLine("\nTASK N8 - GenerateCountryDataFiles: \n");
            async Task GenerateCountryDataFiles()
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://restcountries.com/v3.1/all";
                    string json = await client.GetStringAsync(apiUrl);
                    var countries = JsonConvert.DeserializeObject<List<dynamic>>(json);

                    foreach (var country in countries)
                    {

                        Console.WriteLine(country.name.common);
                        string fName = $"{country.name.common}.txt";
                        string text = $"Region: {country.region}\nSubregion: {country.subregion}\nLatlng: {string.Join(", ", country.latlng)}\nArea: {country.area}\nPopulation: {country.population}";

                        File.WriteAllText(fName, text);
                    }
                }
            }
            GenerateCountryDataFiles();
        }
    }
}
