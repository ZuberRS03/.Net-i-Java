using ConsoleApp1;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void OkreslonyTest1()
        {
            int minGranica = 1;
            int maxGranica = 11;
            Problem problem = new Problem(5, ziarno: 42, minGranica, maxGranica);
            int capacity = 20;
            Wynik wynik = problem.Rozwiazanie(capacity);

            Assert.IsTrue(wynik.WybranePrzedmioty.Count > 0, "Powinien być przynajmniej jeden przedmiot w plecaku.");

        }

        [TestMethod]
        public void OkreslonyTest2()
        {
            int minGranica = 5;
            int maxGranica = 11;
            Problem problem = new Problem(5, ziarno: 42, minGranica, maxGranica);
            int capacity = 4;
            Wynik wynik = problem.Rozwiazanie(capacity);

            Assert.AreEqual(0, wynik.WybranePrzedmioty.Count, "Plecak powinien być pusty, gdy pojemność < najlżejszy przedmiot.");

        }

        [TestMethod]
        public void OkreslonyTest3()
        {
            // Ręcznie ustalamy testową listę przedmiotów
            int minGranica = 1;
            int maxGranica = 11;
            Problem problem = new Problem(0, ziarno: 42, minGranica, maxGranica);
            problem.Przedmioty = new List<Przedmiot>
            {
                new Przedmiot(10, 5), // Opłacalność 2.0
                new Przedmiot(9, 4),  // Opłacalność 2.5
                new Przedmiot(9, 3),  // Opłacalność 3.0
                new Przedmiot(7, 6)   // Opłacalność ~1.17
            };

            int capacity = 10;
            Wynik wynik = problem.Rozwiazanie(capacity);

            Assert.AreEqual(2, wynik.WybranePrzedmioty.Count, "Powinny być wybrane dwa przedmioty.");
            Assert.AreEqual(19, wynik.WartoscCalkowita, "Suma wartości powinna wynosić 19.");
            Assert.AreEqual(8, wynik.WagaCalkowita, "Suma wag powinna wynosić 8.");


        }

        [TestMethod]
        public void WlasnyTest1()
        {
            int minGranica = 1;
            int maxGranica = 11;
            Problem problem = new Problem(0, ziarno: 42, minGranica, maxGranica);
            problem.Przedmioty = new List<Przedmiot>
            {
                new Przedmiot(5, 5), 
                new Przedmiot(5, 5),  
                new Przedmiot(5, 5),  
                new Przedmiot(5, 5)   
            };

            int capacity = 10;
            Wynik wynik = problem.Rozwiazanie(capacity);

            Assert.AreEqual(2, wynik.WybranePrzedmioty.Count, "Powinny być wybrane dwa przedmioty.");
            Assert.AreEqual(10, wynik.WartoscCalkowita, "Suma wartości powinna wynosić 19.");
            Assert.AreEqual(10, wynik.WagaCalkowita, "Suma wag powinna wynosić 8.");

        }

        [TestMethod]
        public void WlasnyTest2()
        {
            int minGranica = 1;
            int maxGranica = 11;
            Problem problem = new Problem(0, ziarno: 42, minGranica, maxGranica);
            problem.Przedmioty = new List<Przedmiot>();

            int capacity = 10;
            Wynik wynik = problem.Rozwiazanie(capacity);

            Assert.AreEqual(0, wynik.WybranePrzedmioty.Count, "Plecak powinien być pusty.");
            Assert.AreEqual(0, wynik.WartoscCalkowita, "Suma wartości powinna wynosić 0.");
            Assert.AreEqual(0, wynik.WagaCalkowita, "Suma wag powinna wynosić 0.");

        }
    }

}
