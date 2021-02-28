using RestApiClientApp_v1.Models;
using RestApiClientApp_v1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestApiClientApp_v1.Business
{
    public class GuessTheCountryGame
    {

        public void StartGame()
        {     
            int inputId;
            int input_prev = 0;
            bool ValidationContinentId = false;
            List<Continent> contList;
            List<Country> countryList = new List<Country>();
            bool dongu = true;

            Console.WriteLine("Ülke tahmin etmece oyununa hoşgeldiniz!");

            contList = ReferentialAPIInfo.GetContinents();

            if (contList == null)
            {
                Console.WriteLine("Kıta bilgisi bulunamadı!");
                return;
            }

            while (dongu == true)
            {
                ShowContinents(contList);

                Console.WriteLine("Oyunu oynamak istediğiniz kıtayı seçin ve başında yazan numarayı yazın:");
                inputId = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("");
                ValidationContinentId = ValidateContinentId(inputId, contList);

                while (!ValidationContinentId)
                {
                    Console.WriteLine("Lütfen yukarıda yazan sayılardan birini giriniz:");
                    inputId = Convert.ToInt16(Console.ReadLine());
                    ValidationContinentId = ValidateContinentId(inputId, contList);
                }

                if (inputId != input_prev)
                    countryList = ReferentialAPIInfo.GetCountriesByContinentId(GetContinentCode(contList, inputId));

                input_prev = inputId;

                if (countryList == null)
                {
                    Console.WriteLine("Ülke bilgisi bulunamadı!");
                    continue;
                }

                //ShowCountriesByContinentId(countryList);

                List<Choice<Country>> choices = SetChoices(countryList, 3);
                Choice<Country> answer = choices.SingleOrDefault(x => x.IsAnswer == true);
                Question<City> question = SetQuestion(choices);

                Console.WriteLine("' {0} 'şehri aşağıdaki ülkelerden hangisindedir?", question.Description);
                ShowChoices(choices);
                string input_s = Console.ReadLine();
                if (input_s.ToLower().Equals(answer.Letter.ToString().ToLower()))
                    Console.WriteLine("Tebrikler, bildiniz !!!");
                else
                    Console.WriteLine("Yanlış cevabı seçtiniz. Doğru cevap {0} olacaktı...", answer.Description);

                Console.WriteLine("Tekrar oynamak ister misiniz? (Yes - y / No - n)");
                if (!Console.ReadLine().Equals("y"))
                    break;

            }
        }
        
        private static Question<City> SetQuestion(List<Choice<Country>> choices)
        {
            Choice<Country> answer = choices.SingleOrDefault(x => x.IsAnswer == true);

            List<City> cityList = ReferentialAPIInfo.GetCitiesByCountryId(answer.ChoiceClass.Code);
            Question<City> question = new Question<City>();
            City randomCity = new City();


            if (cityList == null)
            {
                Console.WriteLine("{0} için şehir bilgisi bulunamadı!", answer.ChoiceClass.Value);
            }
            else
            {
                randomCity = PickSomeInRandomOrder(cityList, 1).Single();
                question.ChoiceClass = randomCity;
                question.Description = randomCity.Value;
            }

            return question;
        }

        private static List<Choice<Country>> SetChoices(List<Country> countryList, int choiceNum)
        {
            Random _random = new Random();
            List<Choice<Country>> choices = new List<Choice<Country>>();
            char c1 = 'A';

            List<Country> randomCountries = PickSomeInRandomOrder(countryList, choiceNum).ToList();

            for (int i = 0; i < randomCountries.Count; i++)
            {
                Choice<Country> choice = new Choice<Country>();
                choice.Letter = c1;
                choice.Description = randomCountries[i].Value;
                choice.ChoiceClass = randomCountries[i];
                choices.Add(choice);
                c1++;
            }

            choices[_random.Next(0, choices.Count)].IsAnswer = true;

            return choices;
        }

        private static void ShowChoices(List<Choice<Country>> choices)
        {
            char c1 = 'A';
            for (int i = 0; i < choices.Count; i++)
            {
                if (i == 0)
                {
                    c1 = 'A';
                    Console.WriteLine($"{c1,-1} - {choices[i].Description,-20}");
                }
                else
                {
                    c1++;
                    Console.WriteLine($"{c1,-1} - {choices[i].Description,-20}");
                }
            }
        }

        public static IEnumerable<SomeType> PickSomeInRandomOrder<SomeType>(IEnumerable<SomeType> someTypes, int maxCount)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            Dictionary<double, SomeType> randomSortTable = new Dictionary<double, SomeType>();

            foreach (SomeType someType in someTypes)
                randomSortTable[random.NextDouble()] = someType;

            return randomSortTable.OrderBy(KVP => KVP.Key).Take(maxCount).Select(KVP => KVP.Value);
        }

        private static void ShowCountriesByContinentId(List<Country> countryList)
        {
            foreach (var item in countryList)
            {
                Console.WriteLine($"{item.Code,-10} {item.Value,-40}");
            }
        }

        private static string GetContinentCode(List<Continent> contList, int inputId)
        {
            foreach (var item in contList)
            {
                if (item.Id == inputId)
                    return item.Key;
            }
            return "NotFound";
        }

        private static bool ValidateContinentId(int inputId, List<Continent> contList)
        {
            if (inputId > contList.Count)
                return false;
            return true;

        }

        private static void ShowContinents(List<Continent> contList)
        {
            foreach (var item in contList)
            {
                Console.WriteLine($"{item.Id,-10} {item.Value,-40}");
            }
        }
    
    }
}
