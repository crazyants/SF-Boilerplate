namespace SimpleFramework.Core.Abstraction.Entitys
{
	/// <summary>
	/// 包含标记删除的接口
	/// </summary>
	public interface IHaveDeleted {
		/// <summary>
		/// 标记已删除
		/// </summary>
		bool Deleted { get; set; }
	}
}
