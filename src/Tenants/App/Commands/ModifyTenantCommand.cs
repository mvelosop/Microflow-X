using System;

namespace Tenants.App.Commands
{
	public class ModifyTenantCommand
	{
		public ModifyTenantCommand(int id, string name, byte[] rowVersion)
		{
			Id = id;
			Name = name ?? throw new ArgumentNullException(nameof(name));
			RowVersion = rowVersion ?? throw new ArgumentNullException(nameof(rowVersion));

			if (rowVersion.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(rowVersion));
			if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
		}

		public int Id { get; }

		public string Name { get; }

		public byte[] RowVersion { get; }
	}
}