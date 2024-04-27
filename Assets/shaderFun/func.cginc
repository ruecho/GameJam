//UNITY_SHADER_NO_UPGRADE
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED


UNITY_INSTANCING_BUFFER_START(Props)
UNITY_DEFINE_INSTANCED_PROP(
float4 my_tab[40], [40])// creates array of size 2
UNITY_INSTANCING_BUFFER_END(Props)

float dist(float2 pos, float2 target)
{
    float2 d = pos - target;
    return d.x * d.x + d.y * d.y;
}

void MyFunction_float(float2 UV, float2 A, float2 B, float2 C, float2 D, float2 E ,out float Out)
{
    float da = dist(UV, A);
    float db = dist(UV, B);
    float dc = dist(UV, C);
    float dd = dist(UV, D);
    float de = dist(UV, E);
    Out = min(min(min(da, db), min(dc, dd)), de);
}
void MyFunction2_float(float2 UV, out float Out)
{
    float val = 1000.0;
    float4 array[] = UNITY_ACCESS_INSTANCED_PROP(Props, my_tab);
        for (int i = 0; i < 40;i++)
        {
            float2 p;
            p.x = array[i].x;
            p.y = array[i].z;
            float d = dist(UV, p);
            val = min(val, d);
            i++;
        }
    
    
    
    Out = val*2.0;
}

#endif //MYHLSLINCLUDE_INCLUDED