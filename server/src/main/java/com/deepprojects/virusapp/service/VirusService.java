package com.deepprojects.virusapp.service;

import com.deepprojects.virusapp.domain.AccountEntity;
import com.deepprojects.virusapp.domain.VirusEntity;
import com.deepprojects.virusapp.exception.ForbiddenException;
import com.deepprojects.virusapp.function.AuthenticatedUser;
import com.deepprojects.virusapp.function.VirusConverter;
import com.deepprojects.virusapp.repository.VirusRepository;
import com.deepprojects.virusapp.rest.request.CreateVirusRequest;
import com.deepprojects.virusapp.rest.response.Virus;
import com.deepprojects.virusapp.util.RandomUtils;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.util.Optional;

@Service
@Slf4j
@RequiredArgsConstructor
public class VirusService {

    private final AuthService authService;
    private final VirusConverter virusConverter;
    private final VirusRepository virusRepository;
    private final ImageService imageService;
    @Value("${virus.stats.damage.min}")
    private final Float damageMinValue;
    @Value("${virus.stats.damage.max}")
    private final Float damageMaxValue;
    @Value("${virus.stats.projectile-speed.min}")
    private final Float projectileSpeedMinValue;
    @Value("${virus.stats.projectile-speed.max}")
    private final Float projectileSpeedMaxValue;
    @Value("${virus.stats.speed.min}")
    private final Float speedMinValue;
    @Value("${virus.stats.speed.max}")
    private final Float speedMaxValue;
    @Value("${virus.stats.life-points.min}")
    private final Float lifePointsMinValue;
    @Value("${virus.stats.life-points.max}")
    private final Float lifePointsMaxValue;

    public Virus createVirus(CreateVirusRequest createVirusRequest) {
        int seed = RandomUtils.generateSeed();
        RandomUtils randomUtils = RandomUtils.getInstance(String.valueOf(seed));
        AuthenticatedUser currentUser = authService.getCurrentUser();

        VirusEntity virusEntity = VirusEntity
                .builder()
                .seed(String.valueOf(seed))
                .name(createVirusRequest.getName())
                .damage(randomUtils.generateFloat(damageMinValue, damageMaxValue))
                .projectileSpeed(randomUtils.generateFloat(projectileSpeedMinValue, projectileSpeedMaxValue))
                .speed(randomUtils.generateFloat(speedMinValue, speedMaxValue))
                .lifePoints(randomUtils.generateFloat(lifePointsMinValue, lifePointsMaxValue))
                .owner(AccountEntity.builder().id(currentUser.getUserId()).build())
                .build();

        VirusEntity savedEntity = virusRepository.save(virusEntity);

        return virusConverter.toVirus(savedEntity);
    }

    public void uploadImage(MultipartFile multipartFile, Long virusId) {
        Optional<VirusEntity> byId = virusRepository.findById(virusId);

        if (byId.isPresent()) {
            VirusEntity virusEntity = byId.get();
            AuthenticatedUser currentUser = authService.getCurrentUser();
            if (!virusEntity.getOwner().getId().equals(currentUser.getUserId())) {
                throw new ForbiddenException();
            }
            String key = imageService.uploadImage(multipartFile, virusEntity.getId(), virusEntity.getSeed());
            virusEntity.setImage(key);
            virusRepository.save(virusEntity);
        }
    }

    public Virus getOne() {
        long count = virusRepository.count();

        return virusConverter.toVirus(virusRepository.getOne(RandomUtils.getLongBetween(count)));
    }
}
