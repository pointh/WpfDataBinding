using System.Collections.ObjectModel;
using WpfDataBinding;

namespace TestProject1
{
    public class UnitTest1
    {
        Person storedPerson = new Person() 
        { 
            Jmeno = "Jan", 
            Prijmeni = "Uhlíø", 
            Narozeni = DateTime.Parse("1/1/2000"), 
            _ID = new Guid("ca761232ed4211cebacd00aa0057b223") 
        };

        public UnitTest1()
        {              
        }

    
        [Fact]
        public void SaveCommandAddsNewPerson()
        {
            // Arrange
            Person.AllPersons.Clear();
            Person.AllPersons.Add(storedPerson);

            Person p = new Person()
            {
                Narozeni = DateTime.Now,
                Jmeno = "Josef",
                Prijmeni = "Novák",
                _ID= Guid.NewGuid(),
            };

            // Act
            p.SaveCommand.Execute(p);

            // Assert
            // V kolekci jsou oba záznamy
            Assert.Contains(Person.AllPersons, t => t.Prijmeni == "Uhlíø");
            Assert.Contains(Person.AllPersons, t => t.Prijmeni == "Novák");
            Assert.Equal(expected: 2, Person.AllPersons.Count);

        }

        [Fact]
        public void SaveCommandUpdatesExistingPerson()
        {
            // Arrange
            Person.AllPersons.Clear();
            Person.AllPersons.Add(storedPerson);

            Person x = new Person()
            {
                _ID = storedPerson._ID,
                Jmeno = "Vlastimil",
                Prijmeni = storedPerson.Prijmeni,
                Narozeni = storedPerson.Narozeni
            };

            // Act
            x.SaveCommand.Execute(x);

            // Assert
            // _ID se po editaci záznamu zmìní,
            // proto neplatí t._ID == x._ID!!
            // 
            // Existuje zmìnìný záznam ...
            Assert.Contains(Person.AllPersons,  
                t => t.Jmeno== x.Jmeno &&
                    t.Prijmeni == x.Prijmeni &&
                    t.Narozeni == x.Narozeni);
            
            // ... a je stále jenom jeden
            Assert.Single(Person.AllPersons);     
        }
    }
}