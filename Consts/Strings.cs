namespace MonteKarloWPFApp1.Consts
{
    public static class Strings
    {
        public static readonly string FormTextBoxesBC_AB_Msg_Str = "BC и AB должны быть целыми числами";
        public static readonly string FormSpacesMsg_Str = "Отступы должны быть целыми числами";
        public static readonly string FormSpaces_RestrictsMsg_Str = "Отступы должны быть больше 0, но меньше 20";
        public static readonly string ABIsntGreaterThanBC_Msg_Str = "Длина AB должна быть больше или равна длине BC";
        public static readonly string FigureIsntDrawed_Msg_Str = "Фигура не отрисована";
        public static readonly string CalcIsntExecuted_Msg_Str = "Расчеты не выполнены";

        public static readonly string StartReport_Str = "Отчет за {0}";
        public static readonly string Report_S_ByFormuls_Str = "Площадь, вычисленная математически - {0}";
        public static readonly string Report_S_ByFormuls_MeasuredTime_Str = "Время, потраченное на вычисление площади математически - {0} мс";
        public static readonly string Report_S_ByMonteCarlo_Str = "Площадь, вычисленная методом Монте Карло:";
        public static readonly string Report_Info_Item_ForMonteCarlo_Str = "Кол-во сгенерированых точек - {0}, площадь - {1}, время - {2} мс, погрешность - {3} %";
        public static readonly string EndReport_Str = "Выполненные расчеты";
    }
}