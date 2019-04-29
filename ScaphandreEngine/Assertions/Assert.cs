namespace ScaphandreEngine.Assertions
{
    public static class Assert
    {
        public static void Fails(string message = "Assertion failed!")
        {
            throw new AssertionException(message);
        }

        public static void IsFalse(bool condition, string message = "Condition should be false!")
        {
            if (!condition) return;
            Fails(message);
        }

        public static void IsTrue(bool condition, string message = "Condition should be true!")
        {
            if (condition) return;
            Fails(message);
        }

        public static void AreEqual(object obj1, object obj2, string message = "Objects should be equals!")
        {
            if (obj1 == obj2) return;
            Fails(message);
        }

        public static void AreNotEqual(object obj1, object obj2, string message = "Objects should not be equals!")
        {
            if (obj1 != obj2) return;
            Fails(message);
        }

        public static void IsNull(object obj, string message = "Object should be null!")
        {
            if (obj == null) return;
            Fails(message);
        }

        public static void IsNotNull(object obj, string message = "Object should not be null!")
        {
            if (obj != null) return;
            Fails(message);
        }
    }
}
