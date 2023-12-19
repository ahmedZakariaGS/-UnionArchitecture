namespace Domain.Commen
{
    public abstract class BaseEntity
    {
        public DateTime InsertDate { get; set; }
        public decimal InsertBy { get; set; }

        public decimal? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public bool IsDeleted { get; set; }

    }

}
