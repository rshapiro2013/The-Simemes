using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoSize
{
    public string file_id;
    public int width;
    public int height;
    public int file_size;
}

public class UserProfilePhotos
{
    public int total_count;
    public List<List<PhotoSize>> photos;
}
