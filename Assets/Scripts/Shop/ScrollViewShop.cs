using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewShop : MonoBehaviour
{
	// Start is called before the first frame update
	static ScrollRect _scrollRect;

	void Start()
	{
		_scrollRect = GetComponent<ScrollRect>();
		//
	}

	public void StopScroll()
    {
		_scrollRect.vertical = false;
	}
	public void ContinueScroll()
	{
		_scrollRect.vertical = true;
	}
}
