package com.deepprojects.virusapp.util;

import java.security.SecureRandom;

public class RandomUtils {

    private final SecureRandom secureRandom;

    private RandomUtils(String seed) {
        this.secureRandom = new SecureRandom(seed.getBytes());
    }

    public static RandomUtils getInstance(String seed) {
        return new RandomUtils(seed);
    }

    public static Float generateFloat(float min, float max, String seed) {
        return new RandomUtils(seed).generateFloat(min, max);
    }

    public static int generateSeed() {
        return org.apache.commons.lang3.RandomUtils.nextInt(1, Integer.MAX_VALUE);
    }

    public static Long getLongBetween(long count) {
        return org.apache.commons.lang3.RandomUtils.nextLong(1, count + 1);
    }

    public Float generateFloat(float min, float max) {
        return min + secureRandom.nextFloat() * (max - min);
    }
}
