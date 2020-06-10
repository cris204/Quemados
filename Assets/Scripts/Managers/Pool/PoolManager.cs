using System;
using System.Collections.Generic;

public static class PoolsOfType<T> where T : class
{
	private static Dictionary<string, SimplePool<T>> pools = null;

	public static SimplePool<T> FindPool(string poolName = null)
	{
		SimplePool<T> result;

		if (pools == null) {
			pools = new Dictionary<string, SimplePool<T>>();

			result = new SimplePool<T>();
			pools.Add(poolName, result);
		} else if (!pools.TryGetValue(poolName, out result)) {
			result = new SimplePool<T>();
			pools.Add(poolName, result);
		}

		return result;
	}
}

public class PoolManager : Singleton<PoolManager>
{
	public void CreatePool<T>(string poolName = null) where T : class
	{
		this.CheckPoolName(typeof(T), ref poolName);
		PoolsOfType<T>.FindPool(poolName);
	}

	public SimplePool<T> GetPool<T>( string poolName = null ) where T : class
	{
		this.CheckPoolName(typeof(T), ref poolName);
		return PoolsOfType<T>.FindPool( poolName );
	}

	public void Push<T>( T obj, string poolName = null) where T : class
	{
		this.CheckPoolName(typeof(T), ref poolName);
		PoolsOfType<T>.FindPool( poolName ).Push( obj );
	}

	public T[] Pop<T>(int quantity, string poolName = null) where T : class
	{
		this.CheckPoolName(typeof(T), ref poolName);
		return PoolsOfType<T>.FindPool(poolName).Pop(quantity);
	}

	public T Pop<T>(string poolName = null) where T : class
	{
		this.CheckPoolName(typeof(T), ref poolName);
		return PoolsOfType<T>.FindPool(poolName).Pop();
	}

	public string CheckPoolName(Type type, ref string poolName)
	{
		if (string.IsNullOrEmpty(poolName)) {
			poolName = type.Name;
		}
		return poolName;
	}

}
