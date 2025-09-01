namespace BTH.Models
{
    public class BMI
    {
        public double Height { get; set; }   // chiều cao (mét)
        public double Weight { get; set; }   // cân nặng (kg)
        public double Result { get; set; }   // chỉ số BMI
        public string Category { get; set; } = "";

        public void Calculate()
        {
            if (Height > 0)
            {
                Result = Weight / (Height * Height);

                if (Result < 18.5)
                    Category = "Gầy";
                else if (Result < 25)
                    Category = "Bình thường";
                else if (Result < 30)
                    Category = "Thừa cân";
                else
                    Category = "Béo phì";
            }
            else
            {
                Result = 0;
                Category = "Chiều cao không hợp lệ";
            }
        }
    }
}
