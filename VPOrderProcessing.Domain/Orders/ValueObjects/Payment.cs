namespace VPOrderProcessor.Domain.Orders.ValueObjects
{
    public record Payment(long PaymentAmount, string MethodOfPaymentId);
}
