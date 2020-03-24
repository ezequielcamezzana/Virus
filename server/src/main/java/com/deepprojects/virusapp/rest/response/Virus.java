package com.deepprojects.virusapp.rest.response;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@AllArgsConstructor
@NoArgsConstructor
@Data
@Builder
public class Virus {

    private Long id;
    private String name;
    private Float damage;
    private Float projectileSpeed;
    private Float speed;
    private Float lifePoints;
    private String seed;
    private String image;
}
