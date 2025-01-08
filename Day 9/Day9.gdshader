shader_type canvas_item;

uniform sampler2D noise : hint_white;
uniform vec4 line : hint_color;
uniform vec4 background : hint_color;
uniform vec4 dots : hint_color;
uniform vec4 fill_1 : hint_color;
uniform vec4 fill_2 : hint_color;
uniform vec4 fill_3 : hint_color;

bool near(float value, float target, float diff)
{
	return value >= target - diff && value <= target + diff;
}

bool draw_wave(vec2 uv, float scale, float freq, float offset, float width, float height, float thickness)
{
	float timescale = TIME * scale;
	return near(uv.y, (sin(uv.x * freq + (offset + timescale))+height) / width, thickness * texture(noise, uv).r);
}

void fragment()
{
	COLOR = background;
	
	vec2 uvc  = vec2(UV.x - TIME * 0.1, UV.y);
	
	if (texture(noise, uvc).r > mix(0.4, 0.6, abs(sin(TIME * 0.1))))
		COLOR = dots;
	
	bool line_1 = draw_wave(UV, 0.5, 3.0, 0.5, 2.0, 1.0, 0.15);
	bool line_2 = draw_wave(UV, 0.6, 2.0, 3, 3.0, 1.5, 0.17);
	bool line_3 = draw_wave(UV, 0.4, 1.0, 2.5, 2.0, 1.4, 0.13);
	bool line_4 = draw_wave(UV, .55, 4.0, 4.5, 1.0, 0.5, 0.18);
		
	bool wave_1 = draw_wave(UV, 0.5, 3.0, 0.5, 2.0, 1.0, 0.12);
	bool wave_2 = draw_wave(UV, 0.6, 2.0, 3, 3.0, 1.5, 0.14);
	bool wave_3 = draw_wave(UV, 0.4, 1.0, 2.5, 2.0, 1.4, 0.11);
	bool wave_4 = draw_wave(UV, .55, 4.0, 4.5, 1.0, 0.5, 0.11);
		
	if (line_1 || line_2 || line_3 || line_4)
		COLOR = line;
		
	if (wave_1 || wave_3)
		COLOR = fill_1;
		
	if (wave_2 || wave_4)
		COLOR = fill_2;
		
	if ((line_1 && line_2) || (line_1 && line_3) || (line_1 && line_4) || (line_2 && line_3) || (line_2 && line_4) || (line_3 && line_4))
		COLOR = line;
		
	if ((wave_1 && wave_2) || (wave_1 && wave_3) || (wave_1 && wave_4) || (wave_2 && wave_3) || (wave_2 && wave_4) || (wave_3 && wave_4))
		COLOR = fill_3;
		
	if ((line_1 && line_2 && line_3) || (line_1 && line_2 && line_4) || (line_2 && line_3 && line_4) || (line_1 && line_3 && line_4))
		COLOR = line;
}