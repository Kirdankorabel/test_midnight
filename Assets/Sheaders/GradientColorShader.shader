Shader"Custom/SmoothGradientWithAlpha"
{
    Properties
    {
        _ColorTop ("Top Color", Color) = (1,0,0,1)
        _ColorBottom ("Bottom Color", Color) = (0,0,1,1)
        _TopPosition ("Top Position", Float) = 1.0
        _BottomPosition ("Bottom Position", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Blend
SrcAlpha OneMinusSrcAlpha // Добавлено смешивание для поддержки прозрачности
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f
{
    float height : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

fixed4 _ColorTop;
fixed4 _ColorBottom;
float _TopPosition;
float _BottomPosition;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.height = v.vertex.y;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    float lerpFactor = (i.height - _BottomPosition) / (_TopPosition - _BottomPosition);
    lerpFactor = clamp(lerpFactor, 0.0, 1.0); // Убеждаемся, что фактор находится между 0 и 1
    return lerp(_ColorBottom, _ColorTop, lerpFactor); // Интерполяция включает альфа-канал
}
            ENDCG
        }
    }
}
