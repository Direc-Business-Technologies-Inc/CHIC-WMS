namespace DataManager.Models.Configurations;

public class Configurations
{
	public int Id { get; set; }
	public string Code { get; set; }
	public string Name { get; set; }
	public int Sequence { get; set; }
	public ICollection<ConfigurationItems>? ConfigurationItems { get; set; }
}
