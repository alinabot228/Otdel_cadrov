//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Соискатели
    {
        public int IDСоискателя { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public Nullable<int> Пол { get; set; }
        public Nullable<int> Возраст { get; set; }
        public string Город { get; set; }
        public Nullable<int> Желаемая_должность { get; set; }
        public string Образование { get; set; }
        public string Опыт_работы { get; set; }
        public Nullable<decimal> Зарплата { get; set; }
    
        public virtual Должности Должности { get; set; }
        public virtual Полы Полы { get; set; }
    }
}
