using Newtonsoft.Json.Linq;
using NPI.Metier;
using NPI.Metier.Implementations;
using System.Security.Cryptography;

namespace NPI.Test
{
    [TestClass]
    public class UnitTest
    {

        [TestMethod]
        public void calculer_valeur_Vide()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.ThrowsException<Exception>(() => npiMetier.Calculer(""));
        }

        [TestMethod]
        public void calculer_une_seul_valeur_5_afficher_5()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.AreEqual(5, npiMetier.Calculer("5"));

        }

        [TestMethod]
        public void Somme_de_5_3()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.AreEqual(8, npiMetier.Calculer("5 3 +"));
        }

        [TestMethod]
        public void Division_de_10_2()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.AreEqual(5, npiMetier.Calculer("10 2 /"));
        }

        [TestMethod]
        public void Soustraction_de_5_3()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.AreEqual(2, npiMetier.Calculer("5 3 -"));
        }

        [TestMethod]
        public void Multiplication_de_5_3()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.AreEqual(15, npiMetier.Calculer("5 3 *"));
        }

        [TestMethod]
        public void Somme_de_5_3_2()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.AreEqual(10, npiMetier.Calculer("5 3 2 + +"));
        }

        // 1 - (5 + 3) = -7
        [TestMethod]
        public void Somme_de_5_3_Puis_Soustraction_de_1()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.AreEqual(-7, npiMetier.Calculer("1 5 3 + -"));
        }

        //7 - (2 × 3) = 1
        [TestMethod]
        public void Multiplication_de_2_3_Puis_Soustraction_de_7()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.AreEqual(1, npiMetier.Calculer("7 2 3 * -"));
        }

        //// 2+
        [TestMethod]
        public void calculer_2_et_plus()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.ThrowsException<InvalidOperationException>(() => npiMetier.Calculer("7 -"));
        }

        //// 2 5
        [TestMethod]
        public void calculer_2_5()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.ThrowsException<InvalidOperationException>(() => npiMetier.Calculer("7 3"));
        }

        //// 5 0
        [TestMethod]
        public void calculer_5_0()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.ThrowsException<DivideByZeroException>(() => npiMetier.Calculer("7 0 /"));
        }


        [TestMethod]
        public void calculer_19223372036854775807_1()
        {
            NPIMetier npiMetier = new NPIMetier();
            Assert.ThrowsException<Exception>(() => npiMetier.Calculer("19223372036854775807 1 /"));
        }


    }
}