package com.deepprojects.virusapp.function;

import com.deepprojects.virusapp.domain.VirusEntity;
import com.deepprojects.virusapp.rest.response.Virus;
import org.springframework.stereotype.Service;

@Service
public class VirusConverter {

    public Virus toVirus(VirusEntity virusEntity) {
        return Virus
                .builder()
                .id(virusEntity.getId())
                .name(virusEntity.getName())
                .damage(virusEntity.getDamage())
                .lifePoints(virusEntity.getLifePoints())
                .projectileSpeed(virusEntity.getProjectileSpeed())
                .speed(virusEntity.getSpeed())
                .seed(virusEntity.getSeed())
                .image(virusEntity.getImage())
                .build();
    }
}
