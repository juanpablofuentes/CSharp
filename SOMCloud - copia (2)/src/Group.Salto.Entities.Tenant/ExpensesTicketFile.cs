namespace Group.Salto.Entities.Tenant
{
    public class ExpensesTicketFile
    {
        public int ExpenseTicketId { get; set; }
        public int SomFileId { get; set; }

        public ExpensesTickets ExpenseTicket { get; set; }
        public SomFiles SomFile { get; set; }
    }
}
