using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteCycler : MonoBehaviour
{
    [SerializeField]
    public List<Texture2D> sprite_array;

    public float anim_speed;
    public float anim_tracker;
    public int anim_index;

    private RawImage img;

    private void Awake()
    {
        img = GetComponent<RawImage>();
        anim_tracker = Random.Range(0.0f, 1.0f);
    }

    public void Update()
    {
        anim_tracker += Time.deltaTime * anim_speed;
        if (anim_tracker >= 1)
        {
            anim_tracker -= 1;
            anim_index++;
            if (anim_index >= sprite_array.Count)
            {
                anim_index = 0;
            }
            img.texture = sprite_array[anim_index];
        }
    }
}
