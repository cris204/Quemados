using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
	string poolName { get; set; }
	void ReturnToPool();
	void GetFromPool();

}
