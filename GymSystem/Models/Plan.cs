namespace GymSystem.Models
{
    public class Plan
    {


        public int Id { get; set; } 

        public DateTime CreatedAt { get; set; }


        public DateTime? UpdateAt { get; set; }


        public String Name { get; set; } = null!;


        public String Description { get; set; } = null!;



        public decimal Price { get; set; }  



        public int Duration { get; set; }   

        

        public bool IsActive { get; set; }  
    }
}
